using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Data.Repositories.ProductRepository;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product entity, CancellationToken cancellationToken)
    {
      await _context.Products.AddAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _context.Products.Where(x => x.Id == id)
                               .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Products.ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Product entity, CancellationToken cancellationToken)
    {
        await _context.Products.Update(entity)
                               .ReloadAsync(cancellationToken);
    }
}