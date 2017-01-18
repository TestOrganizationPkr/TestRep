using DataAccessLayer.Models;
using System;
using System.Diagnostics.CodeAnalysis;

[assembly: CLSCompliant(true)]

namespace DataAccessLayer.Repository
{
    [ExcludeFromCodeCoverage]
    public class ToDoRepository : Repository<ToDoItem>, IToDoRepository
    {
    }
}
