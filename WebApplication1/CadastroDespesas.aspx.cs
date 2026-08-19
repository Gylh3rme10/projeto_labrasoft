using Google.Apis.Gmail.v1;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1
{
    public partial class CadastroDespesas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarProjetos();
            }
        }
        //mostrar projetos no dropdownlist
        private void CarregarProjetos()
        {
            ddlProjeto.DataSource = Repositorio.ListarProjetos();

            ddlProjeto.DataTextField = "Titulo";
            ddlProjeto.DataValueField = "Id";

            ddlProjeto.DataBind();

            ddlProjeto.Items.Insert(0, new ListItem("-- Selecione um projeto --", ""));
        }
        protected void BtnSalvar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtValorDespesa.Text) ||
                string.IsNullOrWhiteSpace(dataDespesa.Text) ||
                string.IsNullOrWhiteSpace(txtCategoria.Text) ||
                string.IsNullOrWhiteSpace(txtDescricao.Text) ||
                ddlProjeto.SelectedIndex <= 0
                )
            {
                lblMensagem.Text = "Há campos não preenchidos.";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {   //Criar despesa
                Despesas novaDespesa = new Despesas();


                novaDespesa.Categoria = txtCategoria.Text;
                novaDespesa.DataDespesa = DateTime.Parse(dataDespesa.Text);
                novaDespesa.Descricao = txtDescricao.Text;
                int idProjeto = Convert.ToInt32(ddlProjeto.SelectedValue);
                novaDespesa.ProjetoID = idProjeto;
                
                //Converter valor

                decimal valor;
                if (decimal.TryParse(txtValorDespesa.Text, NumberStyles.Number, new CultureInfo("pt-BR"), out valor))
                {
                    novaDespesa.Valor = valor;
                }
                else
                {
                    lblMensagem.Text = "Digite um valor válido para a despesa.";
                    return;
                }

                //Salvar despesa no banco

                Repositorio.InserirDespesa(novaDespesa);

                lblMensagem.Text =
                    "Despesa cadastrada com sucesso! " +
                    "A notificação foi enviada por e-mail.";
                lblMensagem.ForeColor = System.Drawing.Color.Green;

                //Buscar projeto

                Projeto projeto = Repositorio.ListarProjetos()
                .FirstOrDefault(p => p.Id == idProjeto);

                // Criar e-mail

                string destinatario =
                "labrasoft.ifba@gmail.com";

                string assunto =
                    "Nova despesa cadastrada";

                string mensagem =
                    "Uma nova despesa foi cadastrada " +
                    "no Sistema de Bolsistas.\n\n" +

                    "PROJETO\n" +
                    "Título: " +
                    (projeto != null
                        ? projeto.Titulo
                        : "Projeto não encontrado") +
                    "\n\n" +

                    "DESPESA\n" +
                    "Categoria: " +
                    novaDespesa.Categoria +
                    "\n" +

                    "Valor: R$ " +
                    novaDespesa.Valor.ToString("N2") +
                    "\n" +

                    "Data: " +
                    novaDespesa.DataDespesa
                        .ToString("dd/MM/yyyy") +
                    "\n" +

                    "Descrição: " +
                    novaDespesa.Descricao +
                    "\n\n" +

                    "Sistema de Bolsistas";

                //Tentar enviar email

                try
                {
                    GmailServices.EnviarEmail(
                        destinatario,
                        assunto,
                        mensagem
                    );

                    lblMensagem.Text =
                        "Despesa cadastrada com sucesso! " +
                        "A notificação foi enviada por e-mail.";

                    lblMensagem.ForeColor =
                        System.Drawing.Color.Green;
                }
                catch (Exception)
                {
                    // A despesa já foi salva.
                    // Se o Gmail falhar, não cancela o cadastro.

                    lblMensagem.Text =
                        "Despesa cadastrada com sucesso, " +
                        "mas não foi possível enviar a notificação por e-mail.";

                    lblMensagem.ForeColor =
                        System.Drawing.Color.Orange;
                }


                LimparCampos();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Cadastro falhou";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
            }

        }
        private void LimparCampos()
        {
            txtValorDespesa.Text = "";
            txtCategoria.Text = "";
            txtDescricao.Text = "";
            ddlProjeto.SelectedIndex = 0;
        }
        protected void BtnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
    }

}