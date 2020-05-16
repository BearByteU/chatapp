using System.ComponentModel.DataAnnotations;

namespace ChatApp.ViewModels
{
    public class LoginDto
    {
        [Required(ErrorMessage = "email can't be null")]
        public string Email{ get; set; }
        [Required(ErrorMessage = "Password can't be null")]
        public string Password { get; set; }
    }
}
