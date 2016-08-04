using System.Collections.Generic;
namespace ToDoWebApp.Models
{
    public class ToDoList
    {
        public ToDoList()
        {
            this.Items = new List<Domain.Models.ToDoItem>();
        }
        public List<Domain.Models.ToDoItem> Items;
    }
}