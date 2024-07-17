using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Configuracion;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Areas.Admin.Controllers;
using SistemaInventario.Modelos;
using SistemaInventario.Modelos.Especificaciones;
using SistemaInventario.Modelos.ViewModels;
using System.Diagnostics;

namespace SistemaInventario.Controllers
{
    [Area("Inventario")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public HomeController(ILogger<HomeController> logger, IUnidadTrabajo unidadTrabajo)
        {
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index(int pageNumber = 1, string busqueda = "", string busquedaActual = "")
        {
            if (!string.IsNullOrEmpty(busqueda))
            {
                pageNumber = 1;
            }
            else
            {
                busqueda = busquedaActual;
            }

            ViewData["BusquedaActual"] = busqueda;

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            Parametros parametros = new Parametros()
            {
                PageNumber = pageNumber,
                PageSize = 8 // Se muestran 8 productos por pagina
            
            };

            var resultado = _unidadTrabajo.Producto.ObtenerTodosPaginado(parametros);

            if (!string.IsNullOrEmpty(busqueda))
            {
                resultado = _unidadTrabajo.Producto.ObtenerTodosPaginado(parametros, p => p.Descripcion.Contains(busqueda));
            }

            // Para guardar datos en memoria
            ViewData["TotalPaginas"] = resultado.MetaData.TotalPages;
            ViewData["TotalRegistros"] = resultado.MetaData.TotalCount;
            ViewData["PageSize"] = resultado.MetaData.PageSize;
            ViewData["PageNumber"] = pageNumber;
            ViewData["Previo"] = "disabled"; // Clase css para desactivar boton
            ViewData["Siguiente"] = ""; // Clase css para desactivar boton

            if(pageNumber > 1) { ViewData["Previo"] = ""; }
            if (resultado.MetaData.TotalPages <= pageNumber ) { ViewData["Siguiente"] = "disabled"; }

            return View(resultado);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}