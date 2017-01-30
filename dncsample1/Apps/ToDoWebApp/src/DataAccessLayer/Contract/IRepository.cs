using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DataAccessLayer.Contract
{
    public interface IRepository<T> :IDisposable where T :class
    {
        /// <summary>
        /// An interface to get all the records
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Intreface to insert the object 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> Create(T item);

        /// <summary>
        /// Inteface to update the existing object by it's ID
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> Update(T item);

        /// <summary>
        /// Interface to delete the object by it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string id);

        Task<T> Find(string id);
    }
}
