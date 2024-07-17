using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IRepositorio.IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IRepositorio.IBodegaRepositorio Bodega { get; private set; }
        public IRepositorio.ICategoriaRepositorio Categoria { get; private set; }
        public IRepositorio.IMarcaRepositorio Marca { get; private set; }
        public IRepositorio.IProductoRepositorio Producto { get; private set;}
        public IRepositorio.IUsuarioAplicacionRepositorio UsuarioAplicacion { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Bodega = new BodegaRepositorio(_db);
            Categoria = new CategoriaRepositorio(_db);
            Marca = new MarcaRepositorio(_db);
            Producto = new ProductoRepositorio(_db);
            UsuarioAplicacion = new UsuarioAplicacionRepositorio(_db);
        }

        public void Dispose()
        {
            // Liberar lo que se tenga en memoria que no se use
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
