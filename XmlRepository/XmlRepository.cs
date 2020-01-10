using System.Collections.Generic;
using System.Linq;

namespace XmlRepository
{
    public class XmlRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly XmlContext _context;
        private IList<T> _entities;

        public IQueryable<T> Table
        {
            get
            {
                return Entities.AsQueryable();
            }
        }

        public IList<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();

                return _entities;
            }
        }

        public XmlRepository(XmlContext context)
        {
            _context = context;
        }

        public void Delete(T item)
        {
            if (Entities.Any(i => i.Id == item.Id))
            {
                Entities.Remove(item);
                _context.SaveChanges(Entities);
            }
        }

        public T GetById(int id)
        {
            return Entities.FirstOrDefault(x => x.Id == id);
        }

        public T Insert(T item)
        {
            if (item.Id == default(int))
            {
                var lastEntity = Entities.LastOrDefault();
                if (lastEntity != null)
                    item.Id = lastEntity.Id + 1;
                else
                    item.Id = 1;

                Entities.Add(item);
                _context.SaveChanges(Entities);
            }

            return item;
        }

        public T Update(T item)
        {
            _context.SaveChanges(Entities);         
            return item;
        }
    }
}
