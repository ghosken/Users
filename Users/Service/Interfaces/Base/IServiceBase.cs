using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces.Base
{
    public interface IServiceBase<T> where T : class
    {
        T Add(T user);
        IEnumerable<T> GetAll();
        T GetbyId(Guid Id);
    }
}
