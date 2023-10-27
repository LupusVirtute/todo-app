using Moq;
using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;
using TodoListBackend.Commands;
using TodoListBackend.Models;

namespace TodoListTests.Commands;

public class AddTodoCommandTest : MockedDbContext
{
    const string testedTitle = "test";
    const string testedDescription = "test";

    const string testedDateString = "10-26-2023";

    private DateTime testedDate => DateTime.Parse(testedDateString);

    [Test]
    public void AddTodoCommand_AddsTodo_Correctly()
    {
        // Arrange
        SetupDatabase();

        var command = new AddTodoCommand(testedTitle, testedDescription, testedDate);
        var handler = new AddTodoCommandHandler(mockedDbContext.Object);

        // Act
        handler.Handle(command, CancellationToken.None);

        // Assert
        mockedDbSet
            .Verify(x =>
                x.Add(It.Is<Todo>(y => y.Title == testedTitle && y.TodoDate == testedDate && y.Description == testedDescription))
                , Times.Once);
        mockedDbContext.Verify(x => x.SaveChanges(), Times.Once);
    }
}