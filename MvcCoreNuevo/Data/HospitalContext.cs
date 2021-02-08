using Microsoft.EntityFrameworkCore;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Data
{
    public class HospitalContext: DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options)
            :base(options){ }

        public DbSet<Departamento> Departamentos { get; set; }

        public DbSet<VistaDept> VistaDepartamentos { get; set; }
    }
}
