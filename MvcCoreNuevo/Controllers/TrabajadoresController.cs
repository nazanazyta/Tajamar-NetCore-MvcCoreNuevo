using Microsoft.AspNetCore.Mvc;
using MvcCoreNuevo.Models;
using MvcCoreNuevo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Controllers
{
    public class TrabajadoresController : Controller
    {
        private IRepositoryHospital repo;

        public TrabajadoresController(IRepositoryHospital repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PaginarGrupoTrabajadoresSQl(int? posicion, int? salario)
        {
            int numregis = 0;
            if (posicion == null)
            {
                posicion = 1;
            }
            if (salario == null)
            {
                salario = 0;
            }
            List<Trabajador> trabajadores =
                this.repo.GetGrupoTrabajadoresSalarioSQL(posicion.Value, salario.Value, ref numregis);
            ViewData["numregis"] = numregis;
            ViewData["salario"] = salario.Value;
            return View(trabajadores);
        }

        //[HttpPost]
        //public IActionResult PaginarGrupoTrabajadoresSQl(int? posicion, int? salario)
        //{
        //    int numregis = 0;
        //    if (posicion == null)
        //    {
        //        posicion = 1;
        //    }
        //    if (salario == null)
        //    {
        //        salario = 0;
        //        List<Trabajador> trabajadores =
        //            this.repo.GetGrupoTrabajadoresSQL(posicion.Value, ref numregis);
        //        ViewData["numregis"] = numregis;
        //        ViewData["salario"] = salario;
        //        return View(trabajadores);
        //    }
        //    else
        //    {
        //        List<Trabajador> trabajadores =
        //            this.repo.GetGrupoTrabajadoresSalarioSQL(posicion.Value, salario.Value, ref numregis);
        //        ViewData["numregis"] = numregis;
        //        ViewData["salario"] = salario.Value;
        //        return View(trabajadores);
        //    }
        //}
    }
}
