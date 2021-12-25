using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface IAssignService
    {
        IResult Add(Assign assign);
        IResult Update(Assign assign);
        IResult Delete(Assign assign);
        IDataResult<List<Assign>> GetAll();
        IDataResult<Assign> GetById(int id);
        IDataResult<List<AssignDetailDto>> GetAssignDetails(Expression<Func<Assign, bool>> filter = null);
        IResult TransactionalOperation(Assign assign);
    }
}
