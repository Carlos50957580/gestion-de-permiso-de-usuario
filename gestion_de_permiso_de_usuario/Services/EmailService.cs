using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        // Obtener la configuración desde appsettings.json
        var smtpServer = _configuration["EmailSettings:SmtpServer"];
        var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
        var senderEmail = _configuration["EmailSettings:SenderEmail"];
        var senderPassword = _configuration["EmailSettings:SenderPassword"];
        var senderName = _configuration["EmailSettings:SenderName"];

        // Configurar el cliente SMTP
        var smtpClient = new SmtpClient(smtpServer)
        {
            Port = smtpPort,
            Credentials = new NetworkCredential(senderEmail, senderPassword),
            EnableSsl = true // Habilitar SSL/TLS
        };

        // Configurar el mensaje de correo
        var mailMessage = new MailMessage
        {
            From = new MailAddress(senderEmail, senderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true // Indica que el cuerpo del correo está en formato HTML
        };

        // Agregar destinatario
        mailMessage.To.Add(toEmail);

        // Enviar el correo de manera asíncrona
        await smtpClient.SendMailAsync(mailMessage);
    }
}
