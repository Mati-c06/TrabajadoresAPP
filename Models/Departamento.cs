using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajadoresApp.Models
{
    [Table("Departamento")]
    public class Departamento
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("NombreDepartamento")]
        public string NombreDepartamento { get; set; }
    }
}
