using System.ComponentModel.DataAnnotations;

namespace webapi.Models{
    public class ForgotPassword{
        [Required]
        public string Email{get;set;}
    }
}