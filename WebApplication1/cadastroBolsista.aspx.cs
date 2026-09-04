using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
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
            try
            {
                if (!DateTime.TryParse(dateBirth.Text, out DateTime dataNascimento))
                {
                    MostrarMensagem("Digite uma data de nascimento válida.", false);
                    return;
                }

                Bolsista Aluno = new Bolsista();

                Aluno.Nome = txtNome.Text.Trim();
                Aluno.CPF = txtCpf.Text.Trim();
                Aluno.DataNascimento = DateTime.Parse(dateBirth.Text);
                Aluno.Matricula = txtMatricula.Text.Trim();
                Aluno.Sexo = ddlSexo.SelectedValue;

                BolsistaService service = new BolsistaService();

                service.CadastrarBolsista(Aluno);

                MostrarMensagem("Bolsista cadastrado com sucesso!", true);

                //Response.Redirect("CadastroBolsista.aspx");

                AtualizarGrid();
            }
            catch (Exception ex) 
            {
                MostrarMensagem(ex.Message, false);

                //Response.Redirect("CadastroBolsista.aspx");
            }
        }
        private void MostrarMensagem(string mensagem, bool sucesso)
        {
            lblMensagem.Text = mensagem;
            lblMensagem.Visible = true;

            if (sucesso)
                lblMensagem.CssClass = "alert alert-success d-block";
            else
                lblMensagem.CssClass = "alert alert-danger d-block";
        }

        private void AtualizarGrid()
        {
            BolsistasRepository repository = new BolsistasRepository();

            List<BolsistaGridDTO> bolsistas = repository.ListarBolsistasGrid();

            if (bolsistas.Count > 0)
            {
                gvBolsistas.DataSource = bolsistas;
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
            MostrarMensagem("", false);
            lblMensagem.Visible = false;
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