using System;
using System.Linq;

namespace DataAccessLayer
{
    public interface IRepository<T> : IDisposable
      where T : class
    {     
        /// <summary>
        /// Gets all objects from database
        /// </summary>
        /// <returns></returns>
        IQueryable<T> All();
        
        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        /// <returns></returns>
        T Find(params object[] keys);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="parameter">Specified a new object to create.</param>
        /// <returns></returns>
        T Create(T parameter);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="parameter">Specified a existing object to delete.</param>
        int Delete(T parameter);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="parameter">Specified the object to save.</param>
        /// <returns></returns>
        int Update(T parameter);
    }
}
