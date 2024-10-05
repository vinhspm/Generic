using Generic.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Generic.Repository
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private ToDoDb _context;

        private DbSet<T> table;

        public GenericRepository()
        {
            _context = new ToDoDb();
            table = _context.Set<T>();
        }

        public GenericRepository(ToDoDb context)
        {
            _context = context;
            table = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(int id)
        {
            var res = table.Find(id);
            return res;
        }

        public void Insert(T entity)
        {
            table.Add(entity);
            Save();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            Save();
        }

        public void Delete(int id)
        {
            T existing = table.Find(id);

            if (existing != null)
            {
                table.Remove(existing);
            }
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
