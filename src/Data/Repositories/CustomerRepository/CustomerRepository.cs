using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Data.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer entity, CancellationToken cancellationToken)
        {
            await _context.Customers.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _context.Customers.Where(x => x.Id == id)
                              .ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Customers.ToListAsync(cancellationToken);
        }

        public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Customers
                                 .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Customer entity, CancellationToken cancellationToken)
        {
            _context.Customers.Update(entity);                                   

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
