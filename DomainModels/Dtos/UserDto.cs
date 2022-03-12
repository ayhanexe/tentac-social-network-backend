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
        [Required, MaxLength(16), MinLength(6)]
        public string Username { get; set; }

        [Required, EmailAddress, MaxLength(100), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string PasswordAgain { get; set; }
    }
}
