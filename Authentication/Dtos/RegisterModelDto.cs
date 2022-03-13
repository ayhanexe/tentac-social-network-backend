using System.ComponentModel.DataAnnotations;

namespace Authentication.Dtos
{
    public class RegisterModelDto
    {

        [Required(), MaxLength(16), MinLength(6)]
        public string Username { get; set; }

        [Required(ErrorMessage = "ValueIsRequired"), EmailAddress, MaxLength(100), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "ValueIsRequired"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ValueIsRequired"), DataType(DataType.Password), Compare(nameof(Password))]
        public string PasswordAgain { get; set; }
    }
}
