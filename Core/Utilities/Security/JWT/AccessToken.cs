using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    //Bir kullanici istekte bulunurken istek bilgileriyle birlikte, yetkisini de gonderir
    //Bu yetki AccessToken (erisim jetonu) olarak gonderilir.
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
