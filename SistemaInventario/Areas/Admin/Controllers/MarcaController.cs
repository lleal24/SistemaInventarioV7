using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public MarcaController(IUnidadTrabajo  unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Marca marca = new Marca();

            // Crear marca
            if(id == null)
            {
                // Crear una nueva marca
                marca.Estado = true;
                return View(marca);
            }

            // Actualizacion bodega
            marca = await _unidadTrabajo.Marca.Obtener(id.GetValueOrDefault());
            if(marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        /// <summary>
        /// Crea o Actualiza
        /// </summary>
        /// <param name="bodega"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] /*Evitar falsificaciones*/
        public async Task<IActionResult> Upsert(Marca marca)
        {
            if (ModelState.IsValid) /*Valida si todas las prop del modelo OK*/
            {
                // Se trata de un nuevo registro
                if (marca.Id == 0)
                {
                    await _unidadTrabajo.Marca.Agregar(marca);
                    TempData[DS.Exitosa] = "Marca creada exitosamente";
                }
                else
                {
                    _unidadTrabajo.Marca.Actualizar(marca);
                    TempData[DS.Exitosa] = "Marca actualizada exitosamente";
                }
                await _unidadTrabajo.Guardar();
                // Redireccionar a otra pagina
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar Categoria";
            return View(marca);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Marca.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var marcaDb = await _unidadTrabajo.Marca.Obtener(id);

            if (marcaDb == null)
            {
                return Json(new { success = false, message = "Error al borrar marca." });
            }

            _unidadTrabajo.Marca.Remover(marcaDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Marca borrada exitosamente" });
        }

        // Validacion si se inserta o actualiza marca con un nombre ya existente
        // Action name para poder llamar desde js desde la vista Upsert
        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;

            // Obtener el listado de marcas
            var lista = await _unidadTrabajo.Marca.ObtenerTodos();

            // INSERT Valacion para nueva bodega cuando es nueva el Id = 0
            if (id == 0)
            {
                // Valida si encuentra almenos una marca con el mismo nombre
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            // UPDATE Validacion para existentes nombre y si existe con el Id 
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
            }
            // Ya existe una con ese nombre
            if (valor)
            {
                return Json(new { data = true });
            }

            // No se encontraron coincidencias OK
            return Json(new { data = false }); 
        }

        #endregion
    }
}
