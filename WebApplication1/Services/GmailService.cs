using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MimeKit;
using System;
using System.IO;
using System.Threading;

namespace WebApplication1.Services
{
    public class GmailServices
    {
        private static readonly string[] Scopes =
        {
            GmailService.Scope.GmailSend
        };

        private const string ApplicationName = "Sistema de Bolsistas";

        private static Google.Apis.Gmail.v1.GmailService ObterServico()
        {
            UserCredential credential;

            string caminhoCredenciais =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "credentials.json"
                );

            string caminhoToken =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "token"
                );

            using (var stream = new FileStream(
                caminhoCredenciais,
                FileMode.Open,
                FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "usuario",
                    CancellationToken.None,
                    new FileDataStore(caminhoToken, true)
                ).Result;
            }

            return new Google.Apis.Gmail.v1.GmailService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                }
            );
        }

        public static void EnviarEmail(
            string destinatario,
            string assunto,
            string mensagem)
        {
            var service = ObterServico();

            var email = new MimeMessage();

            email.To.Add(MailboxAddress.Parse(destinatario));

            email.Subject = assunto;

            email.Body = new TextPart("plain")
            {
                Text = mensagem
            };

            using (var memoryStream = new MemoryStream())
            {
                email.WriteTo(memoryStream);

                byte[] bytes = memoryStream.ToArray();

                string rawMessage =
                    Convert.ToBase64String(bytes)
                    .Replace("+", "-")
                    .Replace("/", "_")
                    .Replace("=", "");

                var gmailMessage = new Message
                {
                    Raw = rawMessage
                };

                service.Users.Messages
                    .Send(gmailMessage, "me")
                    .Execute();
            }
        }
    }
}