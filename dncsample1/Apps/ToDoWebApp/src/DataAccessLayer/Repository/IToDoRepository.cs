using DataAccessLayer.Models;
using System.Threading.Tasks;
using DataAccessLayer.Contract;
using System.Collections.Generic;

namespace DataAccessLayer.Repository
{
    public interface IToDoRepository : IRepository<ToDoItem>
    {
        new bool Delete(string id);

        new List<ToDoItem> All();

        new ToDoItem Create(ToDoItem item);

        new ToDoItem Update(ToDoItem item);

        new ToDoItem Find(string id);
    }
}
