using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Repositories
{
    public interface IRepositoryHospital
    {
        List<Departamento> GetDepartamentos();

        Departamento BuscarDepartamento(int id);

        VistaDept GetRegistroDepartamento(int posicion);

        int GetNumeroRegistrosVistaDepartamento();

        List<VistaDept> GetGrupoDepartamentos(int posicion);

        Departamento GetDepartamentoPosicion(int posicion, ref int salida);

        List<Departamento> GetGrupoDepartamentosSQL(int posicion, ref int numregis);

        List<Trabajador> GetGrupoTrabajadoresSQL(int posicion, ref int numregis);

        List<Trabajador> GetGrupoTrabajadoresSalarioSQL(int posicion, int salario, ref int numregis);
    }
}
