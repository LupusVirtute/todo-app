using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using TodoListBackend.Database;
using TodoListBackend.Models;
using TodoListBackend.Queries;

namespace TodoListTests.Queries;

internal class GetTodosInRangeQueryTest
{
    private DateTime from = new DateTime(2023, 10, 27);
    private DateTime to = new DateTime(2023, 10, 28);

    private DateTime failed = new DateTime(2023, 10, 26, 10,10,20);
    private DateTime succesfull = new DateTime(2023, 10, 27, 10, 0, 0);

    [Test]
    public async Task GetTodosInRangeQuery_Return1InRange_Success()
    {
        // Arrange
        var mockedDbContext = new Mock<IDBContext>();

        var todos = new List<Todo>
        {
            new Todo {
                TodoDate = failed
            },
            new Todo
            {
                TodoDate = succesfull
            }
        };

        mockedDbContext.Setup(x => x.Todos).ReturnsDbSet(todos);

        var query = new GetTodosInRangeQuery(from,to);
        var command = new GetTodosInRangeQueryHandler(mockedDbContext.Object);

        // Act
        var returned = await command.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(returned.Count == 1);
    }
}
