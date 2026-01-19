using CafeAPI.Data;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CafeDbContext _context;

        public CategoryRepository(CafeDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Category
                .FirstOrDefaultAsync(x => x.CategoryId == id);
        }
    }
}
