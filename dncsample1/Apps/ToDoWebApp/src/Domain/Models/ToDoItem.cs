using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ToDoItem
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
