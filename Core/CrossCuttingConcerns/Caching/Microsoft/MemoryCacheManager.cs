using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //Adapter Pattern: Var olan bir sistemi kendi sistemimize gore uyarlıyoruz
        //memoryCache icerisinde .net tarafından get, add gibi komutlar var ancak biz bu kodları kendimize gore uyarlıyoruz
        //ve daha sonra baska cache, validation vb. sistemlere gecis olacaksa daha kolay olması saglanıyor
        private IMemoryCache _memoryCache;
        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public T Get<T>(string key) // liste vs. donecekse, tipi bilinmiyorsa, bu yontem daha kullanıslı
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key) //object her türlü nesnenin atası
        {
            return _memoryCache.Get(key);
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public bool IsAdd(string key) //cache degeri var mi?
        {
            return _memoryCache.TryGetValue(key, out _); //sadece bellekte olup olmadıgını kontrol etmek icin 'out _' kullandık
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        //calışma anında bellekten silme methodu
        //sınıfın instance'ını bellekten çalışma anında müdahele etmek için reflectionları kullanırız, olmayan instanceları'da olusturabiliriz
        //
        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
