﻿using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CategoriaController(IUnidadTrabajo  unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Categoria categoria = new Categoria();

            // Crear bodega
            if(id == null)
            {
                // Crear una nueva bodega
                categoria.Estado = true;
                return View(categoria);
            }

            // Actualizacion bodega
            categoria = await _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
            if(categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        /// <summary>
        /// Crea o Actualiza
        /// </summary>
        /// <param name="bodega"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] /*Evitar falsificaciones*/
        public async Task<IActionResult> Upsert(Categoria categoria)
        {
            if (ModelState.IsValid) /*Valida si todas las prop del modelo OK*/
            {
                // Se trata de un nuevo registro
                if (categoria.Id == 0)
                {
                    await _unidadTrabajo.Categoria.Agregar(categoria);
                    TempData[DS.Exitosa] = "Categoria creada exitosamente";
                }
                else
                {
                    _unidadTrabajo.Categoria.Actualizar(categoria);
                    TempData[DS.Exitosa] = "Categoria actualizada exitosamente";
                }
                await _unidadTrabajo.Guardar();
                // Redireccionar a otra pagina
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar Categoria";
            return View(categoria);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var categoriaDb = await _unidadTrabajo.Categoria.Obtener(id);

            if (categoriaDb == null)
            {
                return Json(new { success = false, message = "Error al borrar categoria." });
            }

            _unidadTrabajo.Categoria.Remover(categoriaDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categoria borrada exitosamente" });
        }

        // Validacion si se inserta o actualiza bodega con un nombre ya existente
        // Action name para poder llamar desde js desde la vista Upsert
        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;

            // Obtener el listado de bodegas
            var lista = await _unidadTrabajo.Categoria.ObtenerTodos();

            // INSERT Valacion para nueva bodega cuando es nueva el Id = 0
            if (id == 0)
            {
                // Valida si encuentra almenos una bodega con el mismo nombre
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
