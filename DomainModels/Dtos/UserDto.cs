using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "ValueIsRequired"), MaxLength(16), MinLength(6)]
        public string Username { get; set; }

        [Required(ErrorMessage = "ValueIsRequired"), EmailAddress, MaxLength(100), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "ValueIsRequired"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ValueIsRequired"), DataType(DataType.Password), Compare(nameof(Password))]
        public string PasswordAgain { get; set; }
    }
}
