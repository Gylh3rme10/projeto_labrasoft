using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

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
            {
                Despesas novaDespesa = new Despesas();


                novaDespesa.Categoria = txtCategoria.Text;
                novaDespesa.DataDespesa = DateTime.Parse(dataDespesa.Text);
                novaDespesa.Descricao = txtDescricao.Text;
                int idProjeto = Convert.ToInt32(ddlProjeto.SelectedValue);
                novaDespesa.ProjetoID = idProjeto;

                decimal valor;
                if (decimal.TryParse(txtValorDespesa.Text, NumberStyles.Number, new CultureInfo("pt-BR"), out valor))
                {
                    novaDespesa.Valor = valor;
                }
                else
                {
                    lblMensagem.Text = "Digite um valor válido para a verba.";
                    return;
                }

                Repositorio.InserirDespesa(novaDespesa);

                Response.Redirect("CadastroDespesas.aspx");

                lblMensagem.Text = "Cadastro concluído:";
                lblMensagem.ForeColor = System.Drawing.Color.Green;
                
            }
            catch (Exception)
            {
                lblMensagem.Text = "Cadastro falhou";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("CadastroDespesas.aspx");
            }

        }
        private void LimparCampos()
        {
            txtValorDespesa.Text = "";
            txtCategoria.Text = "";
            txtDescricao.Text = "";
            ddlProjeto.SelectedIndex = 0;

            lblMensagem.Text = "";
            lblMensagem.CssClass = "";
        }
        protected void BtnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
    }

}