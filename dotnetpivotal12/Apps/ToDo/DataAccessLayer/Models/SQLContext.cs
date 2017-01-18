using System;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using VCapsParser;

namespace DataAccessLayer.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    [ExcludeFromCodeCoverage]
    public sealed class SqlContext : DbContext
    {
        public SqlContext() : base("name=DefaultConnection") {
            string connectionString = VCapsEnvParser.GetConnectionString(ServiceType.MySql);

            ////You can uncomment the below code to get the json, based on your attribute and then create the connection string from that.
            //if (string.IsNullOrEmpty(connectionString))
            //{
            //    var rawJson = VCapsEnvParser.GetRawData("yourattribute");
            //}
            
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("From Webconfig");
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlContext, DataAccessLayer.Migrations.Configuration>("DefaultConnection"));
            }
            else
            {
                Console.WriteLine("From VCapParser");
                Database.Connection.ConnectionString = connectionString;
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlContext, DataAccessLayer.Migrations.Configuration>());
            }
        }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
