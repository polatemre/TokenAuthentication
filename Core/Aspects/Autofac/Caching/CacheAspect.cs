using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        //sure verilmezse cache'te 60dakika duracak
        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //Business.Concrete.IProductService.GetAll
            var arguments = invocation.Arguments.ToList(); //Methodun parametrelerini al
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; // parametre degerleri varsa yaz (Ankara,5) yoksa Null
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key); //methodu calistirma direk cache'teki degeri return et.
                return;
            }
            invocation.Proceed(); // cache'te yoksa methodu çalıştır
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //cache'e ekle
        }
    }
}
