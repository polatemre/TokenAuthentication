using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    //Classlarda ve methodlarda kullanılabilir, birden fazla kullanılabilir, kalıtım olabilir.
    //invocation: method
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; } //önce loglama mı, auth mu hangisi çalışacak?

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
