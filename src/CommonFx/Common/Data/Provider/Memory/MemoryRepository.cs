using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CommonFx.Common.Data.Model;

namespace CommonFx.Common.Data.Provider.Memory
{
    public class MemoryRepository : ISimpleRepository
    {
        public IDictionary<Type, IList<object>> DicValues { get; set; }

        public MemoryRepository()
        {
            DicValues = new ConcurrentDictionary<Type, IList<object>>();
        }

        public MemoryRepository Init<T>(IList<T> items)
        {
            var type = typeof(T);
            DicValues.Add(type, items.Cast<object>().ToList());
            return this;
        }

        private IList<object> Items<T>()
        {
            var type = typeof(T);
            if (!DicValues.ContainsKey(type))
            {
                Init(new List<T>());
            }
            return DicValues[type];
        }
        
        public IQueryable<T> Query<T>() where T : INbEntity<Guid>
        {
            return Items<T>().Cast<T>().AsQueryable();
        }

        public T Get<T>(Guid id) where T : INbEntity<Guid>
        {
            return Query<T>().SingleOrDefault(x => x.Id == id);
        }

        public void Add<T>(params T[] entities) where T : INbEntity<Guid>
        {
            foreach (var entity in entities)
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }
                Items<T>().Add(entity);
            }
        }

        public void Add<T>(T entity, Guid? id = null) where T : INbEntity<Guid>
        {
            entity.Id = id ?? Guid.NewGuid();
            Items<T>().Add(entity);
        }

        public void Update<T>(params T[] entities) where T : INbEntity<Guid>
        {
            var items = Query<T>().ToList();
            foreach (var entity in entities)
            {
                var theOne = items.SingleOrDefault(x => x.Id == entity.Id);
                if (theOne == null)
                {
                    throw new InvalidOperationException("未找到项" + entity.Id);
                }

                if (ReferenceEquals(entity, theOne))
                {
                    return;
                }

                items.Remove(theOne);
                items.Add(entity);
            }
        }

        public void SaveOrUpdate<T>(params T[] entities) where T : INbEntity<Guid>
        {
            var items = Query<T>().ToList();
            foreach (var entity in entities)
            {
                var theOne = items.SingleOrDefault(x => x.Id == entity.Id);
                if (theOne != null)
                {
                    if (ReferenceEquals(entity, theOne))
                    {
                        return;
                    }
                    items.Remove(theOne);
                    items.Add(entity);
                }
                else
                {
                    items.Add(entity);
                }
            }
        }

        public void Delete<T>(params T[] entities) where T : INbEntity<Guid>
        {
            foreach (var entity in entities)
            {
                if (Items<T>().Contains(entity))
                {
                    Items<T>().Remove(entity);
                }
            }
        }

        public void Flush()
        {
        }

        public void QueryOnly(bool queryOnly)
        {
        }
    }
}
