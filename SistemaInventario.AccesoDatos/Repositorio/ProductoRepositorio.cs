using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.Modelos;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IRepositorio.IProductoRepositorio
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Para pasar al padre : base(db)
        /// </summary>
        /// <param name="db"></param>
        public ProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Actualizar(Producto producto)
        {
            // Busca registro
            var productoBD = _db.Productos.FirstOrDefault(b => b.Id == producto.Id);

            // Actualiza cada propiedad por los datos nuevos pasados en parametro
            if(productoBD != null)
            {
                if(productoBD.ImagenUrl != null)
                {
                    productoBD.ImagenUrl = productoBD.ImagenUrl.ToString();
                }

                productoBD.NumeroSerie = producto.NumeroSerie;
                productoBD.Descripcion = producto.Descripcion;
                productoBD.Estado = producto.Estado;
                productoBD.Precio = producto.Precio;
                productoBD.Costo = producto.Costo;
                productoBD.CategoriaId = producto.CategoriaId;
                productoBD.MarcaId = producto.MarcaId;
                productoBD.PadreId = producto.PadreId;

                _db.SaveChanges();
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropDownLista(string obj)
        {
            if (obj == "Categoria")
            {
                return _db.Categorias.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                }); ;
            }

            if (obj == "Marca")
            {
                return _db.Marcas.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                }); ;
            }

            if (obj == "Producto")
            {
                return _db.Productos.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Descripcion,
                    Value = c.Id.ToString()
                });
            }

            return null;
        }
    }
}
