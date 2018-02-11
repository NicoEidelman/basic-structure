using DAL.Repositories.Interfaces;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CollectionRepository<T>: ICollectionRepository<T> where T: BaseEntity
    {
        private readonly Context _context;
        private readonly DbSet<T> _dbset;

        public CollectionRepository(Context ctx)
        {
            _context = ctx;
            _dbset = ctx.Set<T>();
        }

        public int Add(T entity)
        {
            var e = _dbset.Add(entity);
            SaveChanges();
            return e.Id;
        }

        public T FindById(int id, bool? track = null)
        {
            return track == true ? _dbset.FirstOrDefault(t => t.Id == id) : _dbset.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }
    
        public IEnumerable<T> AddRange(ICollection<T> entities)
        {
            var e = _dbset.AddRange(entities);
            SaveChanges();
            return e;
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> filter = null, bool? track = null)
        {
            IQueryable<T> ret = track == true ? _dbset : _dbset.AsNoTracking();
            return filter == null ? ret : ret.Where(filter);
        }

        public void Update(T entity)
        {
            var oldEntity = FindById(entity.Id);
            if (oldEntity != null)
            {
                entity.CreatedDate = oldEntity.CreatedDate;
                entity.CreatedByName = oldEntity.CreatedByName;
                entity.CreatedById = oldEntity.CreatedById;
            }

            _dbset.AddOrUpdate(entity);
            SaveChanges();
        }

        public void UpdateEntity(T entity, T modifiedEntity)
        {
            _context.Entry(entity).CurrentValues.SetValues(modifiedEntity);
            _context.Entry(entity).State = EntityState.Modified;

            Update(entity);
        }

        public void Delete(int id, bool soft = true)
        {
            var entity = FindById(id, true);
            Delete(entity);
        }

        private void Delete(T entity)
        {
            var e = _context.Entry(entity).State == EntityState.Detached ? FindById(entity.Id, true) : entity;
            _dbset.Remove(e);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
