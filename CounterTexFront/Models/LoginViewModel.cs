using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    public class LoginViewModel
    {
        [DisplayName("Nombre de Usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido", AllowEmptyStrings = false)]
        [EmailAddress]
        public string UserName { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}