using System.Data.Entity;

namespace DataAccessLayer.Models
{
    public sealed class SqlContext : DbContext
    {
        public SqlContext() : base("name=DefaultConnection") { }
        
    }
}
