using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Claims;
using WebApplication1.Services;

namespace WebApplication1
{
	public partial class Site : System.Web.UI.MasterPage
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            string paginaAtual = System.IO.Path.GetFileName(Request.Path);

            if (paginaAtual.Equals("Usuario.aspx", StringComparison.OrdinalIgnoreCase))
            {
                navbarPrincipal.Visible = false;
            }

            // Procura o cookie do JWT
            HttpCookie cookie = Request.Cookies["TokenJWT"];

            if (cookie == null)
            {
                return;
            }

            //Pega o token do cookie
            string token = cookie.Value;

            //cria o serviço
            TokenServices tokenServices = new TokenServices();

            //Valida o JWT e extrai as Claims
            ClaimsPrincipal usuario = tokenServices.ValidarToken(token);

            if (usuario == null)
            {
                return;
            }

            //Identifica o usuario autenticado
            string id = usuario.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string email = usuario.FindFirst(ClaimTypes.Email)?.Value;
            string role = usuario.FindFirst(ClaimTypes.Role)?.Value;

        }
    }
}