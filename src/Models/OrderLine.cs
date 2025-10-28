namespace BugStore.Models;

public class OrderLine : Entity
{   
    public Guid OrderId { get; set; }
    
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    
    public Guid ProductId { get; set; }
    public virtual Product Product { get; private set; }
}