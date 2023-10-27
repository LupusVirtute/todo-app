using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListBackend.Database;
using TodoListBackend.Models;
using TodoListBackend.Queries;

namespace TodoListTests.Queries
{
    internal class GetTodosQueryTest
    {
        [Test]
        public async Task GetTodosQuery_ReturnsAll_Success()
        {
            // Arrange
            var mockedDbContext = new Mock<IDBContext>();

            var mockedTodos = new List<Todo>() {
                new Todo {
                    Id = 1
                }
            };

            mockedDbContext.Setup(x => x.Todos).ReturnsDbSet(mockedTodos);

            var query = new GetTodosQuery();
            var command = new GetTodosQueryHandler(mockedDbContext.Object);

            // Act

            var returned = await command.Handle(query,CancellationToken.None);

            // Assert

            Assert.IsTrue(returned.Count == 1);
            
        }
    }
}
