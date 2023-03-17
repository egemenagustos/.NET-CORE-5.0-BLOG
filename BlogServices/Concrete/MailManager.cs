using BlogEntities.Concrete;
using BlogEntities.Dtos;
using BlogServices.Abstract;
using BlogShared.Utilities.Results.Abstract;
using BlogShared.Utilities.Results.Concrete;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BlogShared.Utilities.Results.ComplexTypes;

namespace BlogServices.Concrete
{
    public class MailManager : IMailService
    {
        private readonly SmtpSettings _settings;

        public MailManager(IOptions<SmtpSettings> smtpSettings)
        {
            _settings = smtpSettings.Value;
        }

        public IResult Send(EmailSendDto emailSendDto)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(_settings.SenderEmail),
                To = { new MailAddress(emailSendDto.Email) },
                Subject = emailSendDto.Subject,
                IsBodyHtml = true,
                Body = emailSendDto.Message
            };
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _settings.Server,
                Port = _settings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtpClient.Send(message);
            return new Result(ResultStates.Success, $"E-postanız başarıyla gönderilmiştir.");
        }

        public IResult SendContactMail(EmailSendDto emailSendDto)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(_settings.SenderEmail),
                To = { new MailAddress("info@egemenagustos.com") },
                Subject = emailSendDto.Subject,
                IsBodyHtml = true,
                Body = $"Gönderen Kişi: {emailSendDto.Name}, Gönderen E-Posta Adresi: {emailSendDto.Email}<br>{emailSendDto.Message}"
            };
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _settings.Server,
                Port = _settings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtpClient.Send(message);
            return new Result(ResultStates.Success, $"E-postanız başarıyla gönderilmiştir.");
        }
    }
}
