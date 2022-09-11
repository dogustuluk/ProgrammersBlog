using Microsoft.Extensions.Options;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class MailManager : IMailService
    {
        private readonly SmtpSettings _smtpSettings;

        public MailManager(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public IResult Send(EmailSendDto emailSendDto)
        {//kullanıcıya gidecek mail
            MailMessage message = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail), //dogus.tuluk@gmail.com
                To = { new MailAddress(emailSendDto.Email) }, //dogus.tuluk2@gmail.com
                Subject = emailSendDto.Subject, //şifremi unuttum | siparişiniz kargolandı
                IsBodyHtml = true, //maillerin ilerleyen süreçte belirli bir standartta gelmesini istemek için(logo,e-imza,...)
                Body = emailSendDto.Message //yeni şifreniz: 123456 | 4568121 nolu siparişiniz kargolanmıştır
            };

            //göndermek için
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpSettings.Server,
                Port = _smtpSettings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,//kendi hesap bilgilerimizi verdiğimiz için false
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtpClient.Send(message);

            return new Result(ResultStatus.Success, "E-Postanız başarıyla gönderilmiştir.");
        }

        public IResult SendContactEmail(EmailSendDto emailSendDto)
        {//admine gelecek mail
            MailMessage message = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail),
                To = {new MailAddress("dogus.tuluk2@gmail.com")},
                Subject = emailSendDto.Subject,
                IsBodyHtml = true, //maillerin ilerleyen süreçte belirli bir standartta gelmesini istemek için(logo,e-imza,...)
                Body = $"Gönderen: {emailSendDto.Name}, Gönderen E-Posta:{emailSendDto.Email} <br>{emailSendDto.Message}"
            };

            //göndermek için
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpSettings.Server,
                Port = _smtpSettings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,//kendi hesap bilgilerimizi verdiğimiz için false
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtpClient.Send(message);

            return new Result(ResultStatus.Success, "E-Postanız başarıyla gönderilmiştir.");
        }
    }
}
