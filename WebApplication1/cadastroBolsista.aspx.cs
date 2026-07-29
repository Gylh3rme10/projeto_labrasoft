using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroBolsista : System.Web.UI.Page
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
                string.IsNullOrWhiteSpace(txtCpf.Text) || 
                string.IsNullOrWhiteSpace(dateBirth.Text) || 
                string.IsNullOrWhiteSpace(txtMatricula.Text) || 
                ddlSexo.SelectedIndex <= 0
                )
            {
                lblMensagem.Text = "Há campos não preenchidos.";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                Bolsista novoAluno = new Bolsista();

                novoAluno.Nome = txtNome.Text;
                novoAluno.CPF = txtCpf.Text;
                novoAluno.DataNascimento = DateTime.Parse(dateBirth.Text);
                novoAluno.Matricula = txtMatricula.Text;
                novoAluno.Sexo = ddlSexo.SelectedValue;

                string resumo = novoAluno.ObterResumo();
                int idadeAluno = novoAluno.CalcularIdade();

                Repositorio.InserirBolsista(novoAluno);

                Response.Redirect("CadastroBolsista.aspx");

                lblMensagem.Text = $"Cadastro concluído: {resumo}";
                lblMensagem.ForeColor = System.Drawing.Color.Green;

                AtualizarGrid();

            } catch (Exception) 
            {
                lblMensagem.Text = "Cadastro falhou";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("CadastroBolsista.aspx");
            }
        }
        private void AtualizarGrid()
        {
            if (Repositorio.ListarBolsistas().Count > 0)
            {
                gvBolsistas.DataSource = Repositorio.ListarBolsistas();
                gvBolsistas.DataBind();
                gvBolsistas.Visible = true;
                BtnFiltrarMulheres.Visible = true;
                BtnOrdenarPorNome.Visible = true;
                BtnMostrarTodos.Visible = true;
            }
            else
            {
                gvBolsistas.Visible = false;
                lblAviso.Text = "Não existem alunos cadastrados.";
                
            }
        }
        private void LimparCampos()
        {
            txtNome.Text = "";
            txtCpf.Text = "";
            txtMatricula.Text = "";
            dateBirth.Text = "";
            ddlSexo.SelectedIndex = 0;

            lblMensagem.Text = "";
            lblMensagem.CssClass = "";
        }
        protected void BtnLimpar_Click(object sender, EventArgs e) 
        {
            LimparCampos();
        }
        protected void FiltrarMulheres(object sender, EventArgs e)
        {
            string sexo = ddlSexo.SelectedValue;
            gvBolsistas.DataSource = Repositorio.ListarBolsistas()
                .Where(b => b.Sexo == "F")
                .ToList();
            gvBolsistas.DataBind();
            lblBotao.Text = "Filtrando por mulheres";
            
        }
        protected void OrdenarPorNome(object sender, EventArgs e)
        {
            gvBolsistas.DataSource = Repositorio.ListarBolsistas()
               .OrderBy(b => b.Nome)
                .ToList();
            gvBolsistas.DataBind();

             lblBotao.Text = "Ordenando por nome";
            
        }
        protected void MostrarTodos(object sender, EventArgs e)
        {
            gvBolsistas.DataSource = Repositorio.ListarBolsistas();
            gvBolsistas.DataBind();

            lblBotao.Text = "Mostrando todos";
            
        }
    }

}