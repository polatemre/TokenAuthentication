using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAssignDal : EfEntityRepositoryBase<Assign, ReCapContext>, IAssignDal
    {
        public List<AssignDetailDto> GetAssignDetails(Expression<Func<Assign, bool>> filter = null)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from a in filter == null ? context.Assigns : context.Assigns.Where(filter)
                             join st in context.Students
                             on a.StudentId equals st.Id
                             join b in context.Books
                             on a.BookId equals b.Id
                             select new AssignDetailDto
                             {
                                 AssignId = a.Id,
                                 BookName = b.Name,
                                 StudentFirstName = st.FirstName,
                                 StudentLastName = st.LastName,
                                 StartTime = a.StartTime,
                                 EndTime = a.EndTime,
                                 IsReturn = a.IsReturn,
                                 ReturnDateTime = a.ReturnDateTime
                             };
                return result.ToList();
            }
        }
    }
}
