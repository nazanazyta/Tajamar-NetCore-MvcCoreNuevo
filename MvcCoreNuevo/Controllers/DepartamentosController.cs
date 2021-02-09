using Microsoft.AspNetCore.Mvc;
using MvcCoreNuevo.Models;
using MvcCoreNuevo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Controllers
{
    public class DepartamentosController : Controller
    {
        private IRepositoryHospital repo;

        public DepartamentosController(IRepositoryHospital repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View(this.repo.BuscarDepartamento(id));
        }

        public IActionResult PaginarVistaDeptRegistro(int? posicion)
        {
            //SI NO EXISTE POSICION, MOSTRAMOS EL PRIMER REGISTRO
            if (posicion == null)
            {
                posicion = 1;
            }
            int ultimo = this.repo.GetNumeroRegistrosVistaDepartamento();
            int siguiente = posicion.Value + 1;
            //DEBEMOS COMPROBAR QUE NO NOS PASAMOS...
            if (siguiente > ultimo)
            {
                siguiente = ultimo;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            VistaDept dept = this.repo.GetRegistroDepartamento(posicion.Value);
            ViewData["ultimo"] = ultimo;
            ViewData["siguiente"] = siguiente;
            ViewData["anterior"] = anterior;
            ViewData["posicion"] = posicion.Value;
            return View(dept);
        }

        public IActionResult PaginarVistaDeptGrupo(int? posicion)
        {
            //COMPROBAMOS SI HEMOS RECIBIDO POSICION
            if (posicion == null)
            {
                posicion = 1;
            }
            //<a href=paginagrupo?posicion=1>Página 1</a>
            //<a href=paginagrupo?posicion=3>Página 2</a>
            //<a href=paginagrupo?posicion=5>Página 3</a>
            //int numeropagina = 1;
            //int numregistros = this.repo.GetNumeroRegistrosVistaDepartamento();
            //BUCLE QUE IRÁ DESDE LA POSICIÓN 1 HASTA NÚMERO
            //DE REGISTROS MOVIÉNDOSE EN LOS ELEMENTOS PAGINADOS (2)
            //String html = "<div>";
            //for (int i = 1; i <= numregistros; i += 2)
            //{
            //    html += "<a href='PaginarVistaDeptGrupo?posicion="
            //        + i + "'>Página " + numeropagina + "</a> | ";
            //    numeropagina++;
            //}
            //html += "</div>";
            //ViewData["paginas"] = html;
            ViewBag.numeropagina = 1;
            ViewBag.totalregistros = this.repo.GetNumeroRegistrosVistaDepartamento();
            List<VistaDept> departamentos = this.repo.GetGrupoDepartamentos(posicion.Value);
            return View(departamentos);
        }

        public IActionResult PaginarRegistroDepartamentoSQL(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int ultimo = 0;
            Departamento departamento = this.repo.GetDepartamentoPosicion(posicion.Value, ref ultimo);
            int siguiente = posicion.Value + 1;
            //DEBEMOS COMPROBAR QUE NO NOS PASAMOS...
            if (siguiente > ultimo)
            {
                siguiente = ultimo;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            ViewData["ultimo"] = ultimo;
            ViewData["siguiente"] = siguiente;
            ViewData["anterior"] = anterior;
            ViewData["posicion"] = posicion.Value;
            return View(departamento);
        }

        public IActionResult PaginarGrupoDepartamentosSQl(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            //le damos un valor por defecto que luego
            //se cambiará de nuevo en el procedimiento
            int numregis = 0;
            List<Departamento> departamentos =
                this.repo.GetGrupoDepartamentosSQL(posicion.Value, ref numregis);
            ViewData["numregis"] = numregis;
            return View(departamentos);
        }
    }
}
