using ChatApp.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChatApp.ViewModels
{
    public class UserDto : IValidatableObject
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "First Name Can't be Null")]
        [RegularExpression(@"^([a-zA-Z]+\s)*[a-zA-Z]+$", ErrorMessage = "Name not valid!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "email can't be null")]
        [RegularExpression(@"(?i)[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "invalid email format!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be null")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$", ErrorMessage ="Password must be atleast 8 character")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword can't be null")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "UserName can't be null")]
        public string UserName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
            {
                yield return new ValidationResult("Password not match!");
            }
        }
    }
}
