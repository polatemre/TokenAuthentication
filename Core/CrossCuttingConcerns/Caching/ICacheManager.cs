using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value, int duration); //duration: cache'te durma suresi
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern); //parametreli methodlar icin orn: key ismi get olanlari sil vb.
    }
}
