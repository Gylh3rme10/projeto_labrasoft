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
                ddlCoordenador.DataSource = Repositorio.Coordenadores;
                ddlCoordenador.DataTextField = "Nome";
                ddlCoordenador.DataValueField = "CPF";
                ddlCoordenador.DataBind();

                lstBolsistas.DataSource = Repositorio.ListarBolsistas();
                lstBolsistas.DataTextField = "Nome";   // O que aparece na lista
                lstBolsistas.DataValueField = "CPF";   // Valor associado ao item
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
                novoProjeto.areaConhecimento = txtAreaConhecimento.Text;
                
                decimal verba;

                if (decimal.TryParse(txtVerba.Text, NumberStyles.Number, new CultureInfo("pt-BR"), out verba))
                {
                    novoProjeto.Verba = verba;
                }
                else
                {
                    lblMensagem.Text = "Digite um valor válido para a verba.";
                    return;
                }
                novoProjeto.Coordenadores = Repositorio.Coordenadores
                    .FirstOrDefault(c => c.CPF == ddlCoordenador.SelectedValue);

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
                Repositorio.Projetos.Add(novoProjeto);

                Response.Redirect("CadastroProjeto.aspx");

                lblMensagem.Text = "Cadastro concluído:";
                lblMensagem.ForeColor = System.Drawing.Color.Green;


            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Cadastro falhou";
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

                Projeto projeto = Repositorio.Projetos[indice];

                StringBuilder sb = new StringBuilder();

                sb.Append("<b>Título:</b> ");
                sb.Append(projeto.Titulo);

                sb.Append("<br/><b>Área de Conhecimento:</b> ");
                sb.Append(projeto.areaConhecimento);

                sb.Append("<br/><b>Verba:</b> ");
                sb.Append(projeto.Verba.ToString("C"));

                sb.Append("<br/><b>Coordenador:</b> ");
                sb.Append(projeto.Coordenadores.Nome);

                sb.Append("<br/><b>Bolsistas:</b><br/>");
                foreach (Bolsista b in projeto.Bolsistas)
                {
                    sb.Append("- " + b.Nome + "<br/>");
                }

                lblDetalhes.Text = sb.ToString();
                pnlDetalhes.Visible = true;
            }
        }
        protected void btnFecharDetalhes_Click(object sender, EventArgs e)
        {
            pnlDetalhes.Visible = false;
        }
        private void AtualizarGrid()
        {
            gvProjetos.DataSource = Repositorio.Projetos;
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