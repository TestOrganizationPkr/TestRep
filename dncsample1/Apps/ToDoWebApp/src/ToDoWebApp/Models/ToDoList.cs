using System.Collections.Generic;


namespace ToDoWebApp.Models
{
    public class ToDoList
    {
        public ToDoList()
        {
            Items = new List<Domain.Models.ToDoItem>();
        }
        public List<Domain.Models.ToDoItem> Items;

        /*$$Web_Razor_Cache_Model$$*/
    }
}
