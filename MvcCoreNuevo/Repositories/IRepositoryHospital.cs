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
    }
}
