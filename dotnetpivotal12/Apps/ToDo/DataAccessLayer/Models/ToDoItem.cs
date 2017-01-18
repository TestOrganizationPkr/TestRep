using System.Diagnostics.CodeAnalysis;

namespace DataAccessLayer.Models
{
    [ExcludeFromCodeCoverage]
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
