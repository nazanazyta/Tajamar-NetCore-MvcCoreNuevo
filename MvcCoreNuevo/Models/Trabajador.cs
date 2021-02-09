using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Models
{
    [Table("trabajadores")]
    public class Trabajador
    {
        [Key]
        [Column("id")]
        public int IdTrabajador { get; set; }
        [Column("apellido")]
        public String Apellido { get; set; }
        [Column("trabajo")]
        public String Trabajo { get; set; }
        [Column("salario")]
        public int Salario { get; set; }
    }
}
