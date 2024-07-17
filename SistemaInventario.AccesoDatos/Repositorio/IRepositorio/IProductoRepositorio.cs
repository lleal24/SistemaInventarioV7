using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventario.Modelos;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        void Actualizar(Producto producto);

        /* Funciona para categoria y marca */
        IEnumerable<SelectListItem> ObtenerTodosDropDownLista(string obj);
        
    }
}
