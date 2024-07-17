namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    // IDisposable = Proporciona un mecanismo para liberar recursos no administrados.
    public interface IUnidadTrabajo : IDisposable
    {
        IBodegaRepositorio Bodega { get; }
        ICategoriaRepositorio Categoria { get; }
        IMarcaRepositorio Marca { get; }
        IProductoRepositorio Producto { get; }
        IUsuarioAplicacionRepositorio UsuarioAplicacion { get; }

        Task Guardar();
    }
}
