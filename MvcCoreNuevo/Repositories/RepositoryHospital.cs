using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

#region PROCEDIMIENTOS DEPT
//CREATE PROCEDURE paginarregistrodepartamento
//(@POSICION INT)
//AS
//    SELECT DEPT_NO, DNOMBRE, LOC FROM VISTADEPT
//	WHERE posicion = @POSICION
//GO

//ALTER PROCEDURE paginarregistrodepartamento
//(@POSICION INT, @REGISTROS INT OUT)
//AS
//    SELECT @REGISTROS = COUNT(DEPT_NO) FROM VISTADEPT
//	SELECT DEPT_NO, DNOMBRE, LOC FROM VISTADEPT
//	WHERE posicion = @POSICION
//GO

//CREATE PROCEDURE paginargrupodepartamentos
//(@POSICION INT, @REGISTROS INT OUT)
//AS
//    SELECT @REGISTROS = COUNT(DEPT_NO) FROM VISTADEPT
//	SELECT DEPT_NO, DNOMBRE, LOC FROM VISTADEPT
//	WHERE POSICION >= @POSICION AND POSICION < (@POSICION + 2)
//GO
#endregion

#region VISTA TRABAJADORES PAGINACIÓN
//CREATE VIEW trabajadores
//AS
//	SELECT ISNULL(EMP_NO, 0) AS ID, APELLIDO, OFICIO AS TRABAJO, SALARIO
//		FROM EMP
//	UNION
//	SELECT DOCTOR_NO, APELLIDO, ESPECIALIDAD, SALARIO
//		FROM DOCTOR
//	UNION
//	SELECT EMPLEADO_NO, APELLIDO, FUNCION, SALARIO
//		FROM PLANTILLA
//GO

//CREATE VIEW trabajadorespaginacion
//AS
//    SELECT
//	CAST(ISNULL(ROW_NUMBER() OVER(ORDER BY ID), 0) AS INT) AS POSICION
//   , trabajadores.* FROM trabajadores
//GO
#endregion

#region PROCEDIMIENTOS TRABAJADORES
//CREATE PROCEDURE paginargrupotrabajadores
//(@POSICION INT, @REGISTROS INT OUT)
//AS
//    SELECT @REGISTROS = COUNT(ID) FROM trabajadorespaginacion
//	SELECT ID, APELLIDO, TRABAJO, SALARIO FROM trabajadorespaginacion
//	WHERE POSICION >= @POSICION AND POSICION < (@POSICION + 4)
//GO

//CREATE PROCEDURE paginargrupotrabajadoressalario
//(@POSICION INT, @SALARIO INT, @REGISTROS INT OUT)
//AS
//    SELECT @REGISTROS = COUNT(ID) FROM trabajadorespaginacion
//		WHERE SALARIO >= @SALARIO
//	SELECT ID, APELLIDO, TRABAJO, SALARIO FROM trabajadorespaginacion
//	WHERE POSICION >= @POSICION AND POSICION < (@POSICION + 3)
//		AND SALARIO >= @SALARIO
//GO
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

        public Departamento GetDepartamentoPosicion(int posicion, ref int salida)
        {
            String sql = "paginarregistrodepartamento @posicion, @registros out";
            SqlParameter pampos = new SqlParameter("@posicion", posicion);
            SqlParameter pamreg = new SqlParameter("@registros", -1);
            pamreg.Direction = System.Data.ParameterDirection.Output;
            Departamento departamento = this.Context.Departamentos
                .FromSqlRaw<Departamento>(sql, pampos, pamreg).AsEnumerable().FirstOrDefault();
            salida = Convert.ToInt32(pamreg.Value);
            return departamento;
        }

        public List<Departamento> GetGrupoDepartamentosSQL(int posicion, ref int numregis)
        {
            String sql = "paginargrupodepartamentos @posicion, @registros out";
            SqlParameter pampos = new SqlParameter("@posicion", posicion);
            SqlParameter pamreg = new SqlParameter("@registros", -1);
            pamreg.Direction = System.Data.ParameterDirection.Output;
            List<Departamento> departamentos = this.Context.Departamentos
                .FromSqlRaw(sql, pampos, pamreg).ToList();
            numregis = Convert.ToInt32(pamreg.Value);
            return departamentos;
        }

        public List<Trabajador> GetGrupoTrabajadoresSQL(int posicion, ref int numregis)
        {
            String sql = "paginargrupotrabajadores @posicion, @registros out";
            SqlParameter pampos = new SqlParameter("@posicion", posicion);
            SqlParameter pamreg = new SqlParameter("@registros", -1);
            pamreg.Direction = System.Data.ParameterDirection.Output;
            List<Trabajador> trabajadores = this.Context.Trabajadores
                .FromSqlRaw(sql, pampos, pamreg).ToList();
            numregis = Convert.ToInt32(pamreg.Value);
            return trabajadores;
        }

        public List<Trabajador> GetGrupoTrabajadoresSalarioSQL
            (int posicion, int salario, ref int numregis)
        {
            String sql = "paginargrupotrabajadoressalario @posicion, @salario, @registros out";
            SqlParameter pampos = new SqlParameter("@posicion", posicion);
            SqlParameter pamsal = new SqlParameter("@salario", salario);
            SqlParameter pamreg = new SqlParameter("@registros", -1);
            pamreg.Direction = System.Data.ParameterDirection.Output;
            List<Trabajador> trabajadores = this.Context.Trabajadores
                .FromSqlRaw<Trabajador>(sql, pampos, pamsal, pamreg).ToList();
            numregis = Convert.ToInt32(pamreg.Value);
            return trabajadores;
        }
    }
}
