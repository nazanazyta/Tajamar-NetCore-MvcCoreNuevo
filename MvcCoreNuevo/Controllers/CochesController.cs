using Microsoft.AspNetCore.Mvc;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Controllers
{
    public class CochesController : Controller
    {
        private List<Coche> GetCoches()
        {
            List<Coche> coches = new List<Coche>();
            Coche car = new Coche
            {
                IdCoche = 1,
                Marca = "Ford",
                Modelo = "Mustang",
                VelocidadMaxima = 320
            };
            coches.Add(car);
            car = new Coche
            {
                IdCoche = 2,
                Marca = "Seat",
                Modelo = "600",
                VelocidadMaxima = 95
            };
            coches.Add(car);
            car = new Coche
            {
                IdCoche = 3,
                Marca = "Fiat",
                Modelo = "Panda",
                VelocidadMaxima = 105
            };
            coches.Add(car);
            car = new Coche
            {
                IdCoche = 4,
                Marca = "Opel",
                Modelo = "Astra",
                VelocidadMaxima = 140
            };
            coches.Add(car);
            car = new Coche
            {
                IdCoche = 5,
                Marca = "Nissan",
                Modelo = "Kaskai",
                VelocidadMaxima = 200
            };
            coches.Add(car);
            return coches;
        }
        public IActionResult Index()
        {
            return View(this.GetCoches());
        }

        public IActionResult CochesAsincronos()
        {
            return View();
        }

        public IActionResult GetCochesPartial()
        {
            List<Coche> coches = this.GetCoches();
            //Busca en su carpeta de controller (Coches) o en Shared
            return PartialView("_CochesPartial", coches);
        }

        public IActionResult GetDetallesPartial(int id) //tiene que llamarse id por el
                                                        //mapa de rutas del startup
        {
            Coche car = this.GetCoches().SingleOrDefault(z => z.IdCoche == id);
            return PartialView("_DetallesCochePartial", car);
        }
    }
}
