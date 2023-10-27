using Microsoft.EntityFrameworkCore;
using TodoListBackend.Models;

namespace TodoListBackend.Database;

public interface IDBContext
{
    DbSet<Todo> Todos { get; }
    public int SaveChanges();
}
