using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{   //Bir kere yazıp her yerde kullanabiliriz.
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of C#
            using (TContext context = new TContext()) //using bitince bellekten silinir. Performans için yapılır.
            {
                var addedEntity = context.Entry(entity); //Veri kaynağından nesneyi eşleştir, referansı yakala.
                addedEntity.State = EntityState.Added; //Ne yapacağını belirtiyoruz.
                context.SaveChanges(); //İşlemi yap.
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext()) //using bitince bellekten silinir. Performans için yapılır.
            {
                var deletedEntity = context.Entry(entity); //Veri kaynağından nesneyi eşleştir, referansı yakala.
                deletedEntity.State = EntityState.Deleted; //Ne yapacağını belirtiyoruz.
                context.SaveChanges(); //İşlemi yap.
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //tablodaki bütün sütunları getirir.
                //Filtre null ise product tablosunun tamamı select * from product. Null değilse filtreyi uygular.
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext()) //using bitince bellekten silinir. Performans için yapılır.
            {
                var updatedEntity = context.Entry(entity); //Veri kaynağından nesneyi eşleştir, referansı yakala.
                updatedEntity.State = EntityState.Modified; //Ne yapacağını belirtiyoruz.
                context.SaveChanges(); //İşlemi yap.
            }
        }

    }
}
