using Backend.API.RESTful.Interfaces;
using System.CodeDom;
using System.Net;
using System.Net.Mail;

namespace Backend.API.RESTful.Services
{
    public class SendEmailService : ISendEmailService
    {
        public async Task EnviarEmailCliente(string emailcliente, string nroCupon)
        {
            string emailDesde = "programacioniv.unlz@gmail.com";
            string emailClave = "ourw bweu qpzc jiuk";
            string servicioGoogle = "smtp.gmail.com";

            try
            {
                SmtpClient smtpClient = new SmtpClient(servicioGoogle);
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(emailDesde, emailClave);
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(emailcliente, "Programación IV");
                mailMessage.To.Add(emailcliente);
                mailMessage.Subject = "Número de cupón asignado";
                mailMessage.Body = $"Su número de cupón es {nroCupon}";

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
            }
        }
    }
}