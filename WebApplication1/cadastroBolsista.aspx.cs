using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class cadastroBolsista : System.Web.UI.Page
    {
        protected void btnSalvar_click(object sender, EventArgs e)
        {
            try
            {
                Bolsista aluno = new Bolsista();
                aluno.Nome = txtNome.Text;
                aluno.CPF = txtCPF.Text;
                aluno.Matricula = txtMatricula.Text;
                aluno.DataNascimento = DateTime.Parse(txtDataNascimento.Text);
                string Resumo = aluno.ObterResumo();
                int Idade = aluno.CalcularIdade();

                lblMensagem.Text = $"Sucesso! {Resumo} Idade: {Idade} anos";
                lblMensagem.ForeColor = System.Drawing.Color.DarkBlue;

            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}