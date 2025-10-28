using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        protected CustomerRepository(DbSet<Customer> dbSet) : base(dbSet)
        {
        }
    }
}
