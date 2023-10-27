using Moq;
using NUnit.Framework;
using TodoListBackend.Commands;
using TodoListBackend.Models;

namespace TodoListTests.Commands;

public class RemoveTodoCommandTest : MockedDbContext
{

    const int testedId = 1;

    [Test]
    public void RemoveTodoCommandTest_RemovesTodo_Correctly()
    {
        // Arrange
        SetupDatabase();

        var command = new RemoveTodoCommand(testedId);
        var handler = new RemoveTodoCommandHandler(mockedDbContext.Object);

        // Act
        handler.Handle(command, CancellationToken.None);

        // Assert
        mockedDbSet
            .Verify(x =>
                x.Remove(It.Is<Todo>(y => y.Id == testedId))
                , Times.Once);
        mockedDbContext.Verify(x => x.SaveChanges(), Times.Once);
    }
}
