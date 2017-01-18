using System;
[assembly: CLSCompliant(true)]

namespace DataAccessLayer.Models
{
    public class ToDoItem
    {
        public string id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
