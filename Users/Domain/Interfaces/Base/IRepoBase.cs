using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IRepoBase<T> where T : class
    {
        T Add(T entity);
        T Get(Guid id);
        IEnumerable<T> GetAll();
    }
}
