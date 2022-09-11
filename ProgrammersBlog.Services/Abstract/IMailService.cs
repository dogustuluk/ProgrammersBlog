using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface IMailService
    {
        IResult Send(EmailSendDto emailSendDto);//Genel e-postalar(şifremi unuttum, bilgilerim değiştirildi, bildirim epostları gibi)
        IResult SendContactEmail(EmailSendDto emailSendDto);//iletişim formunda doldurulan bilgileri kullanarak gerekli bilgileri iletecek bir eposta gönderecek.
    }
}
