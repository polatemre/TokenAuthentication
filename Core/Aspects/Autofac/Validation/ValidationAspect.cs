using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //MethodInterception: methodun neresinde çalışacağını belirlediğimiz yapı.
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) // [ValidationAspect(typeof(validatorType))]
        {
            //defensive coding
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //Validator olduğundan emin oluyoruz.
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }

        //reflection çalışma anında işlem yaptırmaya yarar. 
        // _validatorType: Product
        //CreateInstance: ProductValidator'ın instance'ını oluştur
        //BaseType: AbstractValidator<Product> (ProductValidator'un BaseType'ı)
        //GetGenericArguments()[0]: Product (BaseType'ın generic argümanlarından ilki)
        //invocation.Arguments: Methodun(Add vs.) parametreleri entityType:Product olanlar

        protected override void OnBefore(IInvocation invocation) //doğrulama metodun başında yapılır.
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); 
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
