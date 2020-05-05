using System.ComponentModel.DataAnnotations;

namespace webapi.Models{
    public class UserCellLogin{
    [Required]
    public string Cellphone { get; set; }
 
    [Required]
    public string Password { get; set; }
    
    }
}