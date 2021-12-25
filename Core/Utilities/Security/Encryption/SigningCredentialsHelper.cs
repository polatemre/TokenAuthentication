using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        //ornegin WebApi'nin JWT kullanabilmesi icin verecegimiz SecurityKey'in imzalama nesnesini dondurecek
        //Asp.Net'e (WebApi'ye) ne kullandıgımızı bildiriyoruz: sifreleme olarak securityKey'i, guvenlik olarak Sha256 kullan.
        //Credentials: giris bilgileri(email, password)
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
