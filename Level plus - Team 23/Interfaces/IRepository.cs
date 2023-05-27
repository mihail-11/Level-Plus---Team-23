using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Level_plus___Team_23.Domain;

namespace Level_plus___Team_23.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(Guid? id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
