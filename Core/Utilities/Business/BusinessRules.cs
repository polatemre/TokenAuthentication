using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    //C#'ta static olmayan sınıfta, static method tanımlanabilir. Newlemeden kullanabiliriz.
    //Ancak static olmayan bir method eklersek, newleme yaparız.
    public class BusinessRules
    {
        //params ile istediğiniz kadar IResult tipinde parametre gönderebilirsiniz. Bunun yerine listelerde kullanılabilir
        //logics: iş kuralı
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                    //parametre ile gönderilen iş kurallarından başarısız olanları business'a haber veriyoruz -> ErrorResult
                    //ya da tanımladığımız hata mesajı varsa onu döndürür.
                }
            }
            return null;
        }
    }
}
