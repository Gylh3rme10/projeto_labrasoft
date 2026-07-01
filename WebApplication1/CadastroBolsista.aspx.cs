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
        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            
            try
            {
                Bolsista novoAluno = new Bolsista();

                novoAluno.Nome = txtNome.Text;
                novoAluno.CPF = txtCpf.Text;
                novoAluno.DataNascimento = DateTime.Parse(dateBirth.Text);
                novoAluno.Matricula = txtMatricula.Text;
                
                string resumo = novoAluno.ObterResumo();
                int idadeAluno = novoAluno.CalcularIdade();

                lblMensagem.Text = $"Cadastro concluído: {resumo}";
                lblMensagem.ForeColor = System.Drawing.Color.Green;

            } catch (Exception ex) 
            {
                lblMensagem.Text = "Cadastro falhou";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}