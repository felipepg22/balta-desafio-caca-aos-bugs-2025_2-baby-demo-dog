using BugStore.Data.Repositories.CustomerRepository;
using BugStore.Models;

namespace BugStore.Services.CostumerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<Exception, bool>> AddAsync(Customer entity, CancellationToken cancellationToken)
        {
            try
            {
                await _customerRepository.AddAsync(entity, cancellationToken);
                return Result<Exception, bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<Exception, bool>.Failure(ex);                
            }
            
        }

        public async Task<Result<Exception, bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _customerRepository.DeleteAsync(id, cancellationToken);
                return Result<Exception, bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<Exception, bool>.Failure(ex);
            }
        }

        public async Task<Result<Exception, IEnumerable<Customer>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
               var response = await _customerRepository.GetAllAsync(cancellationToken);
               return Result<Exception, IEnumerable<Customer>>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<Exception, IEnumerable<Customer>>.Failure(ex);
            }
        }

        public async Task<Result<Exception, Customer?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);

                if (customer is null)
                {
                    var notFoundException = new Exception("Customer not found");
                    return Result<Exception, Customer?>.Failure(notFoundException);
                }

                return Result<Exception, Customer?>.Success(customer);
            }
            catch (Exception ex)
            {
                return Result<Exception, Customer?>.Failure(ex);
            }
        }

        public async Task<Result<Exception, bool>> UpdateAsync(Guid id, Customer entity, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await GetByIdAsync(id, cancellationToken);

                if (customer.Error is not null)
                {
                    return Result<Exception, bool>.Failure(customer.Error);
                }

                await _customerRepository.UpdateAsync(entity, cancellationToken);
                return Result<Exception, bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<Exception, bool>.Failure(ex);
            }
        }
    }
}
