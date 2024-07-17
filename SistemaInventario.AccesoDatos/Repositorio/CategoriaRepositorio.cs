using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.Modelos;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class CategoriaRepositorio : Repositorio<Categoria>, IRepositorio.ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Para pasar al padre : base(db)
        /// </summary>
        /// <param name="db"></param>
        public CategoriaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Actualizar(Categoria categoria)
        {
            // Busca registro
            var categoriaBD = _db.Categorias.FirstOrDefault(b => b.Id == categoria.Id);

            // Actualiza cada propiedad por los datos nuevos pasados en parametro
            if(categoriaBD != null)
            {
                categoriaBD.Nombre = categoria.Nombre;
                categoriaBD.Descripcion = categoria.Descripcion;
                categoriaBD.Estado = categoria.Estado;
                _db.SaveChanges();
            }
        }
    }
}
