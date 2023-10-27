using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using TodoListBackend.Database;
using TodoListBackend.Models;
using TodoListBackend.Queries;
using TodoListBackend.Services;

namespace TodoListTests.Queries;

public class GetLatestTodoQueryTest
{
    private DateTime testedNowDate = new DateTime(2023, 10, 28, 9, 10, 11);

    private DateTime succesfullDate = new DateTime(2023, 10, 28, 10, 10, 10);
    private DateTime failedDate = new DateTime(2023, 10, 28, 8, 10, 11);

    [Test]
    public async Task GetLatestTodoQuery_CheckIfGetsLatestTodo_Success()
    {
        // Arrange
        var mockedConfig = new Mock<IConfiguration>();
        mockedConfig.Setup(x => x.GetSection("Todos")["SoonNotifierAmountOfHours"]).Returns("1");

        var mockedTimeService = new Mock<ITimeService>();

        mockedTimeService.Setup(x => x.Now).Returns(testedNowDate);

        var mockedDbContext = new Mock<IDBContext>();

        var mockedTodos = new List<Todo>() {
            new Todo {
                Id = 1,
                TodoDate = succesfullDate
            }
        };

        mockedDbContext.Setup(x => x.Todos).ReturnsDbSet(mockedTodos);

        var query = new GetLatestTodoQuery();
        var handler = new GetLatestTodoQueryHandler(mockedConfig.Object, mockedDbContext.Object, mockedTimeService.Object);

        // Act
        var returned = await handler.Handle(query, CancellationToken.None);

        // Assert

        Assert.IsTrue(returned.First().Id == 1);
    }

    [Test]
    public async Task GetLatestTodoQuery_CheckIfGetsLatestTodo_Failure()
    {
        // Arrange
        var mockedConfig = new Mock<IConfiguration>();
        mockedConfig.Setup(x => x.GetSection("Todos")["SoonNotifierAmountOfHours"]).Returns("1");

        var mockedTimeService = new Mock<ITimeService>();

        mockedTimeService.Setup(x => x.Now).Returns(testedNowDate);

        var mockedDbContext = new Mock<IDBContext>();

        var mockedTodos = new List<Todo>() {
            new Todo {
                Id = 1,
                TodoDate = failedDate
            }
        };

        mockedDbContext.Setup(x => x.Todos).ReturnsDbSet(mockedTodos);

        var query = new GetLatestTodoQuery();
        var handler = new GetLatestTodoQueryHandler(mockedConfig.Object, mockedDbContext.Object, mockedTimeService.Object);

        // Act
        var returned = await handler.Handle(query, CancellationToken.None);

        // Assert

        Assert.IsEmpty(returned);
    }
}
