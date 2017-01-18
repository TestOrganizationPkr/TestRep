using DataAccessLayer.Models;
using System;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DataAccessLayer
{
    [ExcludeFromCodeCoverage]
    public class Repository<TObject> : IRepository<TObject>
       where TObject : class
    {
        /// <summary>
        /// This is a constructor which is used to initialize the context 
        /// </summary>
        public Repository()
        {
            Context = new SqlContext();
        }
        

        private SqlContext Context = null;

        protected DbSet<TObject> DbSet
        {
            get
            {
                return Context.Set<TObject>();
            }
        }

        /// <summary>
        /// This is to dispose the context
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isContext)
        {
            if (isContext & (Context != null))
            {
                Context.Dispose();
                Context = null;
            }
        }

        /// <summary>
        /// Gets all objects from database
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TObject> All()
        {
            return DbSet.AsQueryable();
        }

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        /// <returns></returns>
        public virtual TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="parameter">Specified a new object to create.</param>
        /// <returns></returns>
        public virtual TObject Create(TObject parameter)
        {
            var newEntry = DbSet.Add(parameter);
            Context.SaveChanges();
            return newEntry;
        }

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="parameter">Specified a existing object to delete.</param>
        /// <returns></returns>
        public virtual int Delete(TObject parameter)
        {
            DbSet.Remove(parameter);
            return Context.SaveChanges();
        }

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="parameter">Specified the object to save.</param>
        /// <returns></returns>
        public virtual int Update(TObject parameter)
        {
            var entry = Context.Entry(parameter);
            DbSet.Attach(parameter);
            entry.State = EntityState.Modified;
            return Context.SaveChanges();
        }
    }
}
