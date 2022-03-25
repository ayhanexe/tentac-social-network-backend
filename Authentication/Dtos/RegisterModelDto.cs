using DomainModels.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Dtos
{
    public class RegisterModelDto
    {

        [Required(ErrorMessage = "ValueIsRequired"), MaxLength(16), MinLength(6)]
        public string Username { get; set; }

        [Required(ErrorMessage = "ValueIsRequired")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ValueIsRequired")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "ValueIsRequired"), EmailAddress, MaxLength(100), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "ValueIsRequired"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ValueIsRequired"), DataType(DataType.Password), Compare(nameof(Password))]
        public string PasswordAgain { get; set; }

        [Required(ErrorMessage = "ValueIsRequired")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "ValueIsRequired")]
        public DateTime Birthdate { get; set; }
    }
}
