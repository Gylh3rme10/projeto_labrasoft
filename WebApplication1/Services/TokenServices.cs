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
            "MINHA_CHAVE_SECRETA_MUITO_GRANDE_123456";

        // Gera o token do usuário
        public string GerarToken(int id, string email, string tipoUsuario)
        {
            // Cria a chave de segurança
            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(chaveSecreta)
            );

            // Define como o token será assinado
            var credenciais = new SigningCredentials(
                chave,
                SecurityAlgorithms.HmacSha256
            );

            // Informações armazenadas no JWT
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    id.ToString()
                ),

                new Claim(
                    ClaimTypes.Email,
                    email
                ),

                // Define se é Estudante ou Administrador
                new Claim(
                    ClaimTypes.Role,
                    tipoUsuario
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
    }
}