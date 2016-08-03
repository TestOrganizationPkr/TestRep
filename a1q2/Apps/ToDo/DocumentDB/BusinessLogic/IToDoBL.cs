using System.Threading.Tasks;
using System.Collections.Generic;
namespace BusinessLogic
{
    public interface IToDoBL 
    {
        Task<Domain.Models.ToDoItem> Add(Domain.Models.ToDoItem item);
        Task<bool> Delete(string id);
        void Dispose();
        Task<List<Domain.Models.ToDoItem>> GetAll();
        Task<bool> Update(Domain.Models.ToDoItem item);
    }
}
