using DemoProject.Application.Interface.Repository;
using DemoProject.Domain.Model;
using DemoProject.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Infrastructure.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly DemoProjectDbContext _context;
        public CategoryRepository(DemoProjectDbContext context)
        {
            _context = context;
        }
        public async Task Add(Category entity)
        {
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public Task<IQueryable<Category>> GetAll()
        {
            var categories = _context.Categories.AsQueryable();
            return Task.FromResult(categories);
        }

        public async Task<Category?> GetById(int id)
        {
           return await _context.Categories.FindAsync(id);
        }

        public async Task Update(Category entity)
        {
            _context.Categories.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
