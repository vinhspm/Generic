using Generic.Models;
using Microsoft.EntityFrameworkCore;

namespace Generic.DataContext
{
    public class ToDoDb : DbContext
    {
        public ToDoDb()
        {
        }

        public ToDoDb(DbContextOptions<ToDoDb> options) : base(options) { }

        public DbSet<TodoItem> Todos { get; set; }
    }
}