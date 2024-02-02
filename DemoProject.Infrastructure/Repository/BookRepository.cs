using DemoProject.Application.Interface.Repository;
using DemoProject.Domain.Model;
using DemoProject.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Infrastructure.Repository
{
    public class BookRepository : IRepository<Book>
    {
        private readonly DemoProjectDbContext _context;
        public BookRepository(DemoProjectDbContext context)
        {
            _context = context;
        }
        public async Task Add(Book entity)
        {
            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public Task<IQueryable<Book>> GetAll()
        {
            var books = _context.Books.AsQueryable();
            return Task.FromResult(books);
        }

        public async Task<Book?> GetById(int id)
        {
            var book = await _context.Books.Include(b => b.Category).Where(x => x.Id == id).FirstOrDefaultAsync();
            return book;
        }

        public async Task Update(Book entity)
        {
            _context.Books.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
