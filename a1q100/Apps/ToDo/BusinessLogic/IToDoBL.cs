using System.Collections.Generic;
namespace BusinessLogic
{
    public interface IToDoBL 
    {
        Domain.Models.ToDoItem Add(Domain.Models.ToDoItem item);
        bool Delete(int id);
        void Dispose();
        List<Domain.Models.ToDoItem> GetAll();
        bool Update(Domain.Models.ToDoItem item);
    }
}
