using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IRepository<T> : IDisposable
      where T : class
    {
        /// <summary>
        /// Gets all objects from database
        /// </summary>
        /// <returns></returns>
        Task<ICollection<T>> All();

        /// <summary>
        /// This is to get document by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetById(string id);

        /// <summary>
        /// This is to create a new document
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> Create(T item);

        /// <summary>
        /// This is to update the document data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> Update(string id, T item);

        /// <summary>
        /// This is the delete a document
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string id);
    }
}
