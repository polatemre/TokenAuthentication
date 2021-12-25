using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IAssignDal : IEntityRepository<Assign>
    {
        List<AssignDetailDto> GetAssignDetails(Expression<Func<Assign, bool>> filter = null);
    }
}
