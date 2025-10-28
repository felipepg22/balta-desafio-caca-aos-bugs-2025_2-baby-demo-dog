namespace BugStore.Models;

public class Order : Entity
{   
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; private set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<OrderLine> Lines { get; private set; } = null;

    public void SetCustomer(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        Customer = customer;
        CustomerId = customer.Id;
    }

    public void SetLines(List<OrderLine> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);
        Lines = lines;
    }


}