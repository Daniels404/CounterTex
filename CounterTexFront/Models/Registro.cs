using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CounterTexFront.Models
{

    public class Registro
    {
        [Key]
        public int IdRegistro { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El documento es obligatorio.")]
        [StringLength(20, ErrorMessage = "El documento no puede superar los 20 caracteres.")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "Debe confirmar la contraseña.")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContraseña { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Registro")]
        [HiddenInput]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}

