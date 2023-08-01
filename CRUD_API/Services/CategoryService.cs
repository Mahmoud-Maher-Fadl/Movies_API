using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> Add(Category category)
        {
            await _context.Categories.AddAsync(category);
            _context.SaveChanges();
            return category;
        }

        public Category Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return category;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Category> GetById(byte id)
        {
            return await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
        }

        public Task<bool> IsValidCategory(byte id)
        {
            return _context.Categories.AnyAsync(c => c.Id == id);
        }

        public Category Update(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
            return category;
        }
    }
}
