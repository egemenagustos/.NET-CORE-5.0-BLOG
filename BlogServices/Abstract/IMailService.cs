using BlogEntities.Dtos;
using BlogShared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogServices.Abstract
{
    public interface IMailService
    {
        IResult Send(EmailSendDto emailSendDto);

        IResult SendContactMail(EmailSendDto emailSendDto);
    }
}
