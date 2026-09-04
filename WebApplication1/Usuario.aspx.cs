using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCrypt.Net;
using WebApplication1.Models;
using WebApplication1.Services;

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
            string email = txtEmailLogin.Text.Trim();
            string senha = txtSenhaLogin.Text;

            // Validação dos campos
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha))
            {
                MostrarMensagemLogin("Preencha o e-mail e a senha.", false);
                return;
            }

            // Verifica login
            Usuarios usuario = Repositorio.ValidarLogin(email, senha);

            if (usuario != null)
            {   
                //cria o serviço de token
                TokenServices tokenServices = new TokenServices();

                //Gera o JWT --- Id, E-mail e Role
                string token = tokenServices.GerarToken(usuario.Id, usuario.Email);

                // Guarda o usuário logado na sessão
                Session["Usuario"] = usuario;

                // Guarda o JWT em um cookie
                HttpCookie cookie = new HttpCookie("TokenJWT", token);
                cookie.HttpOnly = true;
                cookie.Secure = Request.IsSecureConnection;

                //adiciona cookie à resposta
                Response.Cookies.Add(cookie);

                // Redireciona para a página principal
                Response.Redirect("CadastroBolsista.aspx");
            }
            else
            {
                MostrarMensagemLogin("E-mail ou senha incorretos.", false);
            }
        }

        // Botão Cadastrar
        protected void btnCadastro_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string email = txtEmail.Text.Trim().ToLower();
            string senha = txtSenha.Text;
            
            // Validação dos campos
            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha))
            {
                MostrarMensagemCadastro("Preencha todos os campos.", false);
                return;
            }

            // Verifica se o e-mail já existe
            if (Repositorio.UsuarioExiste(email))
            {
                MostrarMensagemCadastro(
                    "Já existe um usuário cadastrado com este e-mail.",
                    false);

                return;
            }

            // Gera o hash da senha
            string senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);


            // Cria o usuário
            Usuarios usuario = new Usuarios
            {
                Nome = nome,
                Email = email,
                Senha = senhaHash,
            };

            // Insere no banco
            Repositorio.InserirUsuario(usuario);

            // Limpa os campos
            txtNome.Text = "";
            txtEmail.Text = "";
            txtSenha.Text = "";

            // Volta para o login
            mvUsuario.ActiveViewIndex = 0;

            MostrarMensagemLogin(
                "Cadastro realizado com sucesso!",
                true);
        }
        private void MostrarMensagemLogin(string mensagem, bool sucesso)
        {
            string classe = sucesso
                ? "alert alert-success mt-3"
                : "alert alert-danger mt-3";

            lblMensagemLogin.Text = mensagem;
            lblMensagemLogin.CssClass = classe;
            lblMensagemLogin.Visible = true;
        }

        private void MostrarMensagemCadastro(string mensagem, bool sucesso)
        {
            string classe = sucesso
                ? "alert alert-success mt-3"
                : "alert alert-danger mt-3";

            lblMensagemCadastro.Text = mensagem;
            lblMensagemCadastro.CssClass = classe;
            lblMensagemCadastro.Visible = true;
        }
    }
}