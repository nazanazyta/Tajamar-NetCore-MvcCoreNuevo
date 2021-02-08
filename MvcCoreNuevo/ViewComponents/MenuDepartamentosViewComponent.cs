using Microsoft.AspNetCore.Mvc;
using MvcCoreNuevo.Models;
using MvcCoreNuevo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.ViewComponents
{
    public class MenuDepartamentosViewComponent: ViewComponent
    {
        //PODEMOS TENER TODO LO QUE DESEEMOS, PERO TAMBIÉN
        //UN MÉTODO LLAMADO Task InvokeAsync()
        private IRepositoryHospital repo;

        public MenuDepartamentosViewComponent(IRepositoryHospital repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            return View(departamentos);
        }
    }
}
