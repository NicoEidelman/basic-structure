using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IAuthRepository<T> where T: AuthEntity
    {
        string Add(T entity);

        void Update(T entity);

        void UpdateEntity(T entity, T modifiedEntity);

        void Delete(string id, bool soft = true);

        IEnumerable<T> AddRange(ICollection<T> entities);

        IEnumerable<T> List(Expression<Func<T, bool>> filter = null, bool? track = null);

        T FindById(string id, bool? track = null);
    }
}
