using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class AssignManager : IAssignService
    {
        IAssignDal _assignDal;

        public AssignManager(IAssignDal assignDal)
        {
            _assignDal = assignDal;
        }

        [SecuredOperation("assign.add")]
        [ValidationAspect(typeof(AssignValidator))]
        [CacheRemoveAspect("IAssignService.Get")]
        public IResult Add(Assign assign)
        {
            _assignDal.Add(assign);
            return new SuccessResult(Messages.AddedAssign);

        }

        public IResult Delete(Assign assign)
        {

            _assignDal.Delete(assign);
            return new SuccessResult(Messages.DeletedAssign);

        }

        [CacheAspect(duration: 10)]
        [PerformanceAspect(1)]
        public IDataResult<List<Assign>> GetAll()
        {
            return new SuccessDataResult<List<Assign>>(_assignDal.GetAll());
        }

        public IDataResult<Assign> GetById(int id)
        {
            return new SuccessDataResult<Assign>(_assignDal.Get(c => c.Id == id));
        }

        public IDataResult<List<AssignDetailDto>> GetAssignDetails(Expression<Func<Assign, bool>> filter = null)
        {
            return new SuccessDataResult<List<AssignDetailDto>>(_assignDal.GetAssignDetails(filter));

        }

        [ValidationAspect(typeof(AssignValidator))]
        public IResult Update(Assign assign)
        {
            _assignDal.Update(assign);
            return new SuccessResult(Messages.UpdatedAssign);
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Assign assign)
        {
            _assignDal.Update(assign);
            _assignDal.Add(assign);
            return new SuccessResult(Messages.UpdatedAssign);
        }
    }
}