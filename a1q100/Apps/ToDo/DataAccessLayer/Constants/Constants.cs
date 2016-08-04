namespace DAL.Constants
{
    public class Constants
    {
        public const string SelectAll = "select id, name from todoitems";

        public const string SelectById = "select id, name from todoitems where id = @id";

        public const string Insertion = "insert into todoitems values (@Name)";

        public const string Deletion = "Delete from ToDoItems where id=@Id";

        public const string Updation = "Update ToDoItems set Name=@Name where id=@Id";
    }
}
