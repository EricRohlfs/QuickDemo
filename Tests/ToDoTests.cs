using System.Data;
using Xunit;
using QuickDemo;
using Dapper;
using Moq;
using System.Linq;
using Moq.Dapper;
using System.Collections.Generic;

namespace Tests
{
    public class ToDoTests  
    {

        /// <summary>
        /// Keep a look out to see when Moq.Dapper supports the verify method. Then we can make sure it is not called. 
        /// </summary>
        [Fact]
        public void DoesNotCallDBWhenGivenDefaultValue()
        {
            var cnn = new Mock<IDbConnection>();
            var qd = new ToDoItems(cnn.Object);
            var actual = qd.Get(0);
            Assert.False(actual.Any());
        }

        [Fact]
        public void DoesCallDBWhenGivenNonDefaultValue()
        {
            var todos = new List<ToDos>();
            todos.Add(new ToDos { Id = 1, Description = "Hello" });

            var cnn = new Mock<IDbConnection>();
            cnn.SetupDapper(c => c.Query<ToDos>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), true, null, It.IsAny<CommandType>()))
                      .Returns(todos);
            var qd = new ToDoItems(cnn.Object);
            var actual = qd.Get(1);
            Assert.True(actual.Any());
        }
    }
}
