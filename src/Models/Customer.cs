namespace BugStore.Models;

public class Customer : Entity
{   
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
}