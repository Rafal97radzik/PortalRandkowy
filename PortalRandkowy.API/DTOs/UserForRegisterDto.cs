using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortalRandkowy.API.DTOs
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage ="Nazwa użytkownika jest wymagana")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Hasło jest wymagane")]
        [StringLength(12, MinimumLength =6, ErrorMessage ="Hasło musi się składać od 6 do 12 znaków")]
        public string Password { get; set; }
    }
}
