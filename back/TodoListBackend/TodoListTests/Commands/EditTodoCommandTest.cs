using Moq;
using NUnit.Framework;
using TodoListBackend.Commands;
using TodoListBackend.Models;

namespace TodoListTests.Commands;



public class EditTodoCommandTest : MockedDbContext
{
    const string testedTitle = "test";
    const string testedDescription = "test";

    const string testedDateString = "10-26-2023";
    const int testedId = 1;
    private DateTime testedDate => DateTime.Parse(testedDateString);

    [Test]
    public void EditTodoCommand_EditTodo_EditsCorrectly()
    {
        // Arrange
        SetupDatabase();
        var command = new EditTodoCommand(new Todo
        {
            Id = testedId,
            Title = testedTitle,
            Description = testedDescription,
            TodoDate = testedDate
        });

        var handler = new EditTodoCommandHandler(mockedDbContext.Object);

        // Act
        handler.Handle(command, CancellationToken.None);

        // Assert
        mockedDbSet.Verify(x => x.Update(It.Is<Todo>(y => y.Id == testedId && y.Title == testedTitle && y.Description == testedDescription && y.TodoDate == testedDate)), Times.Once);
        mockedDbContext.Verify(x => x.SaveChanges(), Times.Once);
    }
}
