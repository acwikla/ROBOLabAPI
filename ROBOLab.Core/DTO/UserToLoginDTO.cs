using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.DTO
{
    public class UserToLoginDTO
    {
        public int Id { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Invalid password.")]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email adress.")]
        public string Email { get; set; }
    }
}
