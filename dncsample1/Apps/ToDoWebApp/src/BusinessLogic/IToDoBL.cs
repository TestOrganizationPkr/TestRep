﻿using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IToDoBL
    {
        Domain.Models.ToDoItem Add(Domain.Models.ToDoItem item);
        bool Delete(string id);
        void Dispose();
        List<Domain.Models.ToDoItem> GetAll();
        bool Update(Domain.Models.ToDoItem item);
        string GetItemCount();
bool RemoveItem();
    }
}
