using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using TodoListBackend.Contexts;
using TodoListBackend.Database;
using TodoListBackend.Models;

namespace TodoListTests;

public abstract class MockedDbContext
{
    protected Mock<IDBContext> mockedDbContext = new Mock<IDBContext>();
    
    protected Mock<DbSet<Todo>> mockedDbSet = new Mock<DbSet<Todo>>();

    protected void SetupDatabase() {
        mockedDbContext.Setup(x => x.Todos).Returns(mockedDbSet.Object);
    }
}
