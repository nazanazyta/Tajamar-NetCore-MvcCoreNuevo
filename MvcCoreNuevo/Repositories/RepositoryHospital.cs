using MvcCoreNuevo.Data;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region VISTA DEPARTAMENTOS PAGINACIÓN
//create view vistadept as
//select
//isnull(row_number() over(order by dept_no), 0)
//as posicion
//, isnull(dept.dept_no, 0) as dept_no
//, dept.dnombre, dept.loc from dept
//go

//alter view vistadept as
//select
//cast(isnull(row_number() over(order by dept_no), 0) as int)
//as posicion
//, isnull(dept.dept_no, 0) as dept_no
//, dept.dnombre, dept.loc from dept
//go
#endregion

namespace MvcCoreNuevo.Repositories
{
    public class RepositoryHospital : IRepositoryHospital
    {
        HospitalContext Context;

        public RepositoryHospital(HospitalContext context)
        {
            this.Context = context;
        }

        public List<Departamento> GetDepartamentos()
        {
            return this.Context.Departamentos.ToList();
        }

        public Departamento BuscarDepartamento(int id)
        {
            return this.Context.Departamentos.SingleOrDefault(x => x.Numero == id);
        }

        public VistaDept GetRegistroDepartamento(int posicion)
        {
            return this.Context.VistaDepartamentos
                .Where(x => x.Posicion == posicion).FirstOrDefault();
        }

        public int GetNumeroRegistrosVistaDepartamento()
        {
            return this.Context.VistaDepartamentos.Count();
        }

        public List<VistaDept> GetGrupoDepartamentos(int posicion)
        {
            //select* from vistadept where posicion >= 1 and posicion< (1 + 2)
            var consulta = from datos in this.Context.VistaDepartamentos
                           where datos.Posicion >= posicion
                           && datos.Posicion < (posicion + 2)
                           select datos;
            return consulta.ToList();
        }
    }
}
