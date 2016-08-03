using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DataAccessLayer.Repository
{
    public interface IDefaultRepository : IRepository<ToDoItem>
    {
        Task<bool> Delete(ToDoItem item);

        new Task<ToDoItem> Update(string id, ToDoItem item);
    }
}
