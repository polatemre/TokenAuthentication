using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Business.Constants;

namespace Business.BusinessAspects.Autofac
{
    //JWT
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; //Her bir kisinin http istegi icin, bir HttpContext olusur (thread)

        public SecuredOperation(string roles) // product.add, admin...
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            //productService = ServiceTool.ServiceProvider.GetService<IProductDal>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); //o anki claim rollerini bul
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return; //claimlerin icinde ilgili rol varsa return, yetkisi var
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
