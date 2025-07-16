using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajadoresApp.Models
{
    [Table("Distrito")]
    public class Distrito
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("NombreDistrito")]
        public string NombreDistrito { get; set; }
    }
}
