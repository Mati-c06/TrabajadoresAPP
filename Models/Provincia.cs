using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajadoresApp.Models
{
    [Table("Provincia")]
    public class Provincia
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("NombreProvincia")]
        public string NombreProvincia { get; set; }
    }
}
