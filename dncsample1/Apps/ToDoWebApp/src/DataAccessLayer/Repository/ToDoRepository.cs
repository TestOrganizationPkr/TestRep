using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Contract;
using Microsoft.Azure.Documents;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace DataAccessLayer.Repository
{
    public class ToDoRepository : Repository<ToDoItem>, IToDoRepository
    {
        public ToDoRepository(IOptions<ConfigurationSettings> settings) : base(settings)
        {
        }

        public new bool Delete(string id)
        {
            return base.Delete(id).Result;
        }

        public List<ToDoItem> All()
        {
            return GetAll().Result;
        }

        public new ToDoItem Create(ToDoItem item)
        {
            return base.Create(item).Result;
        }

        public new ToDoItem Update(ToDoItem item)
        {
            if (base.Find(item.id).Result != null)
                return base.Update(item).Result;
            return null;
        }

        public new ToDoItem Find(string id)
        {
            return base.Find(id).Result;
        }
    }
}
