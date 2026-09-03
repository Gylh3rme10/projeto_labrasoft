using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Services
{
    public class TokenServices
    {
        // Chave usada para assinar o JWT
        private readonly string chaveSecreta =
            "LabraSoft_Security_Key_2026_@_Secret_System_v1.0!";

        // Gera o token do usuário
        public string GerarToken(int id, string email)
        {
            //Define a role pelo e-mail
            string role;

            if (email.EndsWith("@labrasoft.com", StringComparison.OrdinalIgnoreCase))
            {
                role = "Admin";
            }
            else
            {
                role = "Bolsista";
            }

            // Cria a chave de segurança
            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(chaveSecreta)
            );

            // Define como o token será assinado
            var credenciais = new SigningCredentials(
                chave,
                SecurityAlgorithms.HmacSha256
            );

            // Cria as Claims --- Informações armazenadas no JWT
            var claims = new[]
            {
                new Claim("UserID",
                    id.ToString()
                ),

                new Claim("Email",
                    email
                ),

                new Claim(ClaimTypes.Role,
                    role
                )
            };

            // Cria o JWT
            var token = new JwtSecurityToken(
                issuer: "WebApplication1",
                audience: "WebApplication1",
                claims: claims,

                // Token válido por 2 horas
                expires: DateTime.UtcNow.AddHours(2),

                signingCredentials: credenciais
            );

            // Transforma o JWT em string
            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }

        public ClaimsPrincipal ValidarToken(string token)
        {
            // Cria o handler do JWT
            var handler = new JwtSecurityTokenHandler();

            //Cria a chave de segurança
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));

            // Configura a validação
            var parametros = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = chave,

                ValidateIssuer = true,
                ValidIssuer = "WebApplication1",

                ValidateAudience = true,
                ValidAudience = "WebApplication1",

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                //Valida o token e extrai as Claims
                return handler.ValidateToken(token, parametros, out SecurityToken tokenValidado);
            }
            catch
            {
                //Token invalido ou expirado
                return null;
            }
        }
    }
}