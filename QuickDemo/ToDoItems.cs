using System.Data;
using Dapper;
using System.Collections.Generic;

namespace QuickDemo
{

    public class ToDos
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }


    public class ToDoItems
    {
        private IDbConnection CNN { get; set; }

        public ToDoItems(IDbConnection cnn)
        {
            CNN = cnn;
        }

        /*
         * How to called a stored procedure using dapper in our project.
         * Wrap the call in a using statement so the call is closed when complete.
         * Notice there are no try catches around the call?  Those are handled via the middleware.
         * Notice as a security practice we don't allow defaults of any type to call the database.
         * 
         * Dapper will automatically open and close the connection string, unless you need more control.
         */
        public IEnumerable<ToDos> Get(int id)
        {
            if (id == 0)
            {
                return new List<ToDos>() as IEnumerable<ToDos>;
            }
            var todo = CNN.Query<ToDos>("dbo.getUsersSP", new { Id = id }, commandType: CommandType.StoredProcedure);
            return todo;
        }

    }
}
