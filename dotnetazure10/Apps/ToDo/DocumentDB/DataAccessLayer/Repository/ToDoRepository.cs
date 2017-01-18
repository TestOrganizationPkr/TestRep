using System;
using System.Configuration;
using DataAccessLayer.Models;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class ToDoRepository : Repository<ToDoItem>, IToDoRepository
    {
        private readonly string Collection = ConfigurationManager.AppSettings["collection"];

        public ToDoRepository() 
        {
            CollectionName = Collection;
        }

        public async Task<bool> Delete(ToDoItem item)
        {
            if (base.GetById(item.id) != null)
                return await base.Delete(item.id);

            return false;
        }

        public override async Task<ToDoItem> Update(string id, ToDoItem item)
        {
            if (!string.IsNullOrEmpty(id) && item != null)
            {
                item.DateModified = DateTime.Now;
                return await base.Update(id, item);
            }
            else
                return null;
        }

        public override async Task<ToDoItem> Create(ToDoItem item)
        {
            item.DateCreated = item.DateModified = DateTime.Now;
            return await base.Create(item);
        }
    }
}
