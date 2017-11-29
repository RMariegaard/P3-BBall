using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem.Database.Interfaces
{
    public interface IDataAccesss<TEntity> where TEntity : class, IDisposable
    {
        TEntity Get(int Id);
        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        int Complete();

    }
}
