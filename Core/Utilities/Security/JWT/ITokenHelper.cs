using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    //JWT: Json Web Token
    //Kullanıcı adı ve parolayı girip apiye istek atıldıgında, eger bilgileri dogru ise CreateToken calisir
    //Kullanıcının veritabanındaki claimlerini olusturacak, JWT uretip geri dondurecek.
    //İlgili kullanıcının claimlerini icerecek bir token uretecek.
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
