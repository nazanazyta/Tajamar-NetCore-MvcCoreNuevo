using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Models
{
    [Table("vistadept")]
    public class VistaDept
    {
        [Column("posicion")]
        //public long Posicion { get; set; } --> int64 en BBDD
        public int Posicion { get; set; }
        [Key]
        [Column("dept_no")]
        public int Numero { get; set; }
        [Column("dnombre")]
        public String Nombre { get; set; }
        [Column("loc")]
        public String Localidad { get; set; }
        //Propiedad que vamos a mostrar/utilizar,
        //pero no viene de la base de datos
        //[NotMapped]
        //public String OtraPropiedad { get; set; }
    }
}
