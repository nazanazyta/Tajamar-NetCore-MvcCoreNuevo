using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Models
{
    [Table("dept")]
    public class Departamento
    {
        [Key]
        [Column("dept_no")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Numero { get; set; }
        [Column("dnombre")]
        public String Nombre { get; set; }
        [Column("loc")]
        public String Localidad { get; set; }
    }
}
