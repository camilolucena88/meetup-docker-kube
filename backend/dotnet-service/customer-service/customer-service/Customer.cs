using System.ComponentModel.DataAnnotations;

namespace CustomerService;

public class Customer
{
    public int Id { get; set; }

    [Required]
    public string Email { get; set; } = "";

    public string? Name { get; set; }
}