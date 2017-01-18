using System;
using System.ComponentModel.DataAnnotations;
[assembly: CLSCompliant(true)]

namespace Domain.Models
{
    public class ToDoItem
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
