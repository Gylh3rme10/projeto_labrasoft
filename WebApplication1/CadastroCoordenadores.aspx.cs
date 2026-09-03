using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroCoordenadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AtualizarGrid();
            }
        }
        protected void BtnSalvar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtCPF.Text) ||
                string.IsNullOrWhiteSpace(txtAtuacao.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                ddlTitulacao.SelectedIndex <= 0
                )
            {
                lblMensagem.Text = "Há campos não preenchidos.";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (Repositorio.CoordenadorExiste(txtCPF.Text))
            {
                lblMensagem.Text = "Já existe um coordenador cadastrado com esse CPF.";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                txtCPF.Text = "";
                return;
            }

            try
            {
                Coordenador novoCoordenador = new Coordenador();

                novoCoordenador.Nome = txtNome.Text;
                novoCoordenador.CPF = txtCPF.Text;
                novoCoordenador.AreaAtuacao = txtAtuacao.Text;
                novoCoordenador.Email = txtEmail.Text;
                novoCoordenador.Titulacao = ddlTitulacao.SelectedValue;

                Repositorio.InserirCoordenador(novoCoordenador);

                Response.Redirect("CadastroBolsista.aspx");

                lblMensagem.Text = "Cadastro concluído:";
                lblMensagem.ForeColor = System.Drawing.Color.Green;

                AtualizarGrid();

            }
            catch (Exception)
            {
                lblMensagem.Text = "Cadastro falhou";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("CadastroCoordenadores.aspx");
            }

        }
        private void AtualizarGrid()
        {
            CoordenadoresRepository repository = new CoordenadoresRepository();

            List<CoordenadorGridDTO> coordendadores = repository.ListarCoordenadoresGrid();

            if (coordendadores.Count > 0)
            {
                gvCoordenadores.DataSource = coordendadores;
                gvCoordenadores.DataBind();
                gvCoordenadores.Visible = true;

                txtPesquisa.Visible = true;
                btnPesquisar.Visible = true;
                BtnMostrarTodos.Visible = true;
            }
            else
            {
                gvCoordenadores.Visible = false;
                lblAviso.Text = "Não existem coordenadores cadastrados.";

            }
        }
        private void LimparCampos()
        {
            txtNome.Text = "";
            txtCPF.Text = "";
            txtEmail.Text = "";
            txtAtuacao.Text = "";
            ddlTitulacao.SelectedIndex = 0;

            lblMensagem.Text = "";
            lblMensagem.CssClass = "";
        }
        protected void BtnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string pesquisa = txtPesquisa.Text.Trim().ToLower();

            List<Coordenador> resultado = Repositorio.ListarCoordenadores()
                .Where(c =>
                    c.Nome.ToLower().Contains(pesquisa) ||
                    c.Titulacao.ToLower().Contains(pesquisa))
                .ToList();

            gvCoordenadores.DataSource = resultado;
            gvCoordenadores.DataBind();

            if (resultado.Count == 0)
                lblAviso.Text = "Nenhum coordenador encontrado.";
            else
                lblAviso.Text = "";
                lblBotao.Text = "";
        }
        protected void MostrarTodos(object sender, EventArgs e)
        {
            gvCoordenadores.DataSource = Repositorio.ListarCoordenadores();
            gvCoordenadores.DataBind();

            lblBotao.Text = "Mostrando todos";
            lblAviso.Text = "";
        }
    }
}