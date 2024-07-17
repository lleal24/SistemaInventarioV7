using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.Modelos;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class MarcaRepositorio : Repositorio<Marca>, IRepositorio.IMarcaRepositorio
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Para pasar al padre : base(db)
        /// </summary>
        /// <param name="db"></param>
        public MarcaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Actualizar(Marca marca)
        {
            // Busca registro
            var marcaBD = _db.Marcas.FirstOrDefault(b => b.Id == marca.Id);

            // Actualiza cada propiedad por los datos nuevos pasados en parametro
            if(marcaBD != null)
            {
                marcaBD.Nombre = marca.Nombre;
                marcaBD.Descripcion = marca.Descripcion;
                marcaBD.Estado = marca.Estado;
                _db.SaveChanges();
            }
        }
    }
}
