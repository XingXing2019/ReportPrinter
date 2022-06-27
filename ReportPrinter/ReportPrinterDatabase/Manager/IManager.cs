using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportPrinterDatabase.Manager
{
    public interface IManager<T>
    {
        Task Post(T obj);
        Task<T> Get(Guid id);
        Task<IList<T>> GetAll();
        Task Put(T obj);
        Task Delete(T obj);
    }
}