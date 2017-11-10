using System.ComponentModel.DataAnnotations;

namespace windforce_corp.ViewModels
{
    public class AuthViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}