using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystem.Database
{
    public class DatabaseController<TEntity> : IDatabaseController<TEntity> where TEntity : class
    {
        protected readonly DatabaseContext _context;

        public DatabaseController(DatabaseContext context)
        {
            this._context = context;
        }


        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }


        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }


        public TEntity Get(int Id)
        {
            return _context.Set<TEntity>().Find(Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
