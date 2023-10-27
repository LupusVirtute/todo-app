
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListBackend.Models;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    [Column("todo_date")]
    public DateTime TodoDate { get; set; }
}
