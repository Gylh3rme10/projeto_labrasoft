using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // Vai do Login para o Cadastro
        protected void lnkCadastro_Click(object sender, EventArgs e)
        {
            mvUsuario.ActiveViewIndex = 1;
        }

        // Vai do Cadastro para o Login
        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            mvUsuario.ActiveViewIndex = 0;
        }

        // Botão Entrar
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Vamos implementar o login aqui depois.
        }

        // Botão Cadastrar
        protected void btnCadastro_Click(object sender, EventArgs e)
        {
            // Vamos implementar o cadastro aqui depois.
        }
    }
}