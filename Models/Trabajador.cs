using System.ComponentModel.DataAnnotations;

namespace TrabajadoresApp.Models
{
    public class Trabajador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Tipo de Documento es obligatorio")]
        [Display(Name = "Tipo Documento")]
        [StringLength(3)]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "El Número de Documento es obligatorio")]
        [Display(Name = "Nro Documento")]
        [StringLength(50)]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(500)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio")]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Required]
        [Display(Name = "Departamento")]
        public int? IdDepartamento { get; set; }

        [Required]
        [Display(Name = "Provincia")]
        public int? IdProvincia { get; set; }

        [Required]
        [Display(Name = "Distrito")]
        public int? IdDistrito { get; set; }

        /* solo para lectura */
        public string? NombreDepartamento { get; set; }
        public string? NombreProvincia { get; set; }
        public string? NombreDistrito { get; set; }
    }
}
