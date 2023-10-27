using Microsoft.EntityFrameworkCore;
using TodoListBackend.Database;
using TodoListBackend.Models;

namespace TodoListBackend.Contexts;

public class TodoDbContext : DbContext, IDBContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {

    }
    public DbSet<Todo> Todos { get; set; } = null!;

}
