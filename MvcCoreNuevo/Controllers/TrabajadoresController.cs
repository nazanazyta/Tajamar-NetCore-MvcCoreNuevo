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
        IRepositoryHospital repo;

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
            List<Trabajador> trabajadores;
            if (salario == null)
            {
                trabajadores =
                    this.repo.GetGrupoTrabajadoresSQL(posicion.Value, ref numregis);
            }
            else
            {
                trabajadores =
                    this.repo.GetGrupoTrabajadoresSQL(posicion.Value, salario.Value, ref numregis);
                ViewData["salario"] = salario.Value;
            }
            ViewData["numregis"] = numregis;
            return View(trabajadores);
        }

        [HttpPost]
        public IActionResult PaginarGrupoTrabajadoresSQl(int salario)
        {
            int numregis = 0;
            int posicion = 1;
            List<Trabajador> trabajadores =
                this.repo.GetGrupoTrabajadoresSQL(posicion, salario, ref numregis);
            ViewData["numregis"] = numregis;
            ViewData["salario"] = salario;
            return View(trabajadores);
        }
    }
}
