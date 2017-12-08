using System;
using System.Collections;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text.RegularExpressions;
using Biblioteca.Entidades;

namespace Biblioteca.Funcoes
{
    public class Email
    {
        public string destinatario { get; set; }
        public string assunto { get; set; }
        public string mensagem { get; set; }

        public string EnviaMensagem(Email variavel)
        {
            try 
            {
                SmtpClient smtp = new SmtpClient();
                NetworkCredential credenciais = new NetworkCredential("usuario@dominio.com", "senha");
                MailMessage msg = new MailMessage();
                MailAddress from = new MailAddress("usuario@dominio.com");

                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credenciais;

                msg.From = from;
                msg.Subject = variavel.assunto;
                msg.IsBodyHtml = true;
                msg.Body = variavel.mensagem;
                msg.To.Add(variavel.destinatario);

                smtp.Send(msg);

                return "Mensagem enviada com sucesso";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}
