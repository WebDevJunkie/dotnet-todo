using DotnetTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetTodo
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
                : base(options)
        { }

        public DbSet<Todo> Todos { get; set; }
    }
}
