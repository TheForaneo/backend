using System.ComponentModel.DataAnnotations;

namespace webapi.Models{
    public class UserEmailLogin{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
 
    [Required]
    public string Password { get; set; }
 
    }
}