using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroProjeto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCoordenador.DataSource = Repositorio.ListarCoordenadores();
                ddlCoordenador.DataTextField = "Nome";
                ddlCoordenador.DataValueField = "Id";
                ddlCoordenador.DataBind();

                lstBolsistas.DataSource = Repositorio.ListarBolsistas();
                lstBolsistas.DataTextField = "Nome";   // O que aparece na lista
                lstBolsistas.DataValueField = "Id";   // Valor associado ao item
                lstBolsistas.DataBind();

                ddlCoordenador.Items.Insert(0,
                    new ListItem("-- Selecione um coordenador --", ""));

                AtualizarGrid();
            }
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
                string.IsNullOrWhiteSpace(txtAreaConhecimento.Text) ||
                string.IsNullOrWhiteSpace(txtVerba.Text)
                )
            {
                lblMensagem.Text = "Há campos não preenchidos.";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                Projeto novoProjeto = new Projeto();

                novoProjeto.Titulo = txtTitulo.Text;
                novoProjeto.AreaConhecimento = txtAreaConhecimento.Text;
                
                decimal verba;

                if (decimal.TryParse(txtVerba.Text, NumberStyles.Number, new CultureInfo("pt-BR"), out verba))
                {
                    novoProjeto.VerbaAprovada = verba;
                }
                else
                {
                    lblMensagem.Text = "Digite um valor válido para a verba.";
                    return;
                }
                int idCoordenador = Convert.ToInt32(ddlCoordenador.SelectedValue);

                novoProjeto.Coordenadores = Repositorio.ListarCoordenadores()
                    .FirstOrDefault(c => c.Id == idCoordenador);
                //terminar substituição de CPF por ID

                // Bolsistas selecionados
                foreach (ListItem item in lstBolsistas.Items)
                {
                    if (item.Selected)
                    {
                        Bolsista bolsista = Repositorio.ListarBolsistas()
                            .FirstOrDefault(b => b.CPF == item.Value);

                        if (bolsista != null)
                        {
                            novoProjeto.Bolsistas.Add(bolsista);
                        }
                    }
                }

                // Salva o projeto
                Repositorio.InserirProjeto(novoProjeto);

                Response.Redirect("CadastroProjeto.aspx");

                lblMensagem.Text = "Cadastro concluído:";
                lblMensagem.ForeColor = System.Drawing.Color.Green;


            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("CadastroProjeto.aspx");
            }
            AtualizarGrid();
        }
        protected void gvProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalhes")
            {
                int indice = Convert.ToInt32(e.CommandArgument);

                // 1. Valida se o índice existe na lista antes de tentar acessar
                if (indice >= 0 && indice < projetos.Count)
                {
                    // Limpa mensagens de erro anteriores se houver
                    lblMensagem.Text = string.Empty;

                    Projeto projeto = projetos[indice];

                    // Trata o nome do Coordenador
                    string nomeCoordenador = projeto.Coordenadores != null
                        ? projeto.Coordenadores.Nome
                        : "<i>Não informado</i>";

                    // Trata a lista de Bolsistas
                    string listaBolsistas = "<i>Nenhum bolsista cadastrado</i>";
                    if (projeto.Bolsistas != null && projeto.Bolsistas.Count > 0)
                    {
                        // Junta o nome dos bolsistas separados por <br/>
                        listaBolsistas = string.Join("<br/>", projeto.Bolsistas.Select(b => "- " + b.Nome));
                    }

                    // Cria os dados no formato Chave/Valor para carregar na GridView
                    var detalhes = new[]
                    {
                        new { Campo = "Título:", Valor = projeto.Titulo },
                        new { Campo = "Área de Conhecimento:", Valor = projeto.AreaConhecimento },
                        new { Campo = "Verba Aprovada:", Valor = projeto.VerbaAprovada.ToString("C") },
                        new { Campo = "Coordenador:", Valor = nomeCoordenador },
                        new { Campo = "Bolsistas:", Valor = listaBolsistas }
                    };

                    // Preenche o GridView e exibe o painel
                    gvDetalhesProjeto.DataSource = detalhes;
                    gvDetalhesProjeto.DataBind();

                    pnlDetalhes.Visible = true;
                }
                else
                {
                    // Exibe a mensagem no label de erro e oculta o painel
                    lblMensagem.Text = "Aviso: O projeto selecionado não existe ou a lista foi atualizada.";
                    pnlDetalhes.Visible = false;
                }
            }
        }
        }
        protected void btnFecharDetalhes_Click(object sender, EventArgs e)
        {
            pnlDetalhes.Visible = false;
        }
        private void AtualizarGrid()
        {
            gvProjetos.DataSource = Repositorio.ListarProjetos();
            gvProjetos.DataBind();
            pnlDetalhes.Visible = false;
            lblDetalhes.Text = "";
        }
        private void LimparCampos()
        {
            txtTitulo.Text = "";
            txtAreaConhecimento.Text = "";
            txtVerba.Text = "";
            ddlCoordenador.SelectedIndex = 0;

            lblMensagem.Text = "";
            lblMensagem.CssClass = "";

            pnlDetalhes.Visible = false;
            lblDetalhes.Text = "";

            foreach (ListItem item in lstBolsistas.Items)
            {
                item.Selected = false;
            }
        }
        protected void BtnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

    }
}