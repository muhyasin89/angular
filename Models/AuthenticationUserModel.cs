using System.ComponentModel.DataAnnotations;

namespace ParkerRandalWeb.Models
{
    public class AuthenticationUserModel
    {
        [Required(ErrorMessage = "Username is Required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = string.Empty;
    }
}