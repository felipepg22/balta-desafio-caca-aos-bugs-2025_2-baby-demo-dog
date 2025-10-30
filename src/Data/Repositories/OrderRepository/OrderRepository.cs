using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Data.Repositories.OrderRepository;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order entity, CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
       await _context.Orders.Where(x => x.Id == id)
                            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken)
  {
        return await _context.Orders.Include(x => x.Customer)
                                    .Include(x => x.Lines)
                                    .ThenInclude(x => x.Product)
                                    .ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Orders.Include(x => x.Customer)
                                    .Include(x => x.Lines)
                                    .ThenInclude(x => x.Product)
                                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Order entity, CancellationToken cancellationToken)
    {
        await _context.Orders.Update(entity)
                             .ReloadAsync(cancellationToken);
    }
}