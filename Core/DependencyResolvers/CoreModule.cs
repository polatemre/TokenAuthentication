using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
    //Uygulama seviyesindeki servis bağımlılıklarımızı çözümleyeceğimiz yer
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache(); // IMemoryCache'in karsiligi, .net core kendisi injection yapıyor
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Http istekten cevaba kadar gecen surede takibi yapacak
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>(); //redis, memcache... gecilecegi zaman burası degisecek
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
