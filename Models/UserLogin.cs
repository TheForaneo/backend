using System.ComponentModel.DataAnnotations;

namespace webapi.Models{
    public class UserLogin{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
 
    [Required]
    public string Password { get; set; }
 
    }
}