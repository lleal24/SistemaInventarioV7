using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class BodegaRepositorio : Repositorio<Bodega>, IRepositorio.IBodegaRepositorio
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Para pasar al padre : base(db)
        /// </summary>
        /// <param name="db"></param>
        public BodegaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Actualizar(Bodega bodega)
        {
            // Busca registro
            var bodegaBD = _db.Bodegas.FirstOrDefault(b => b.Id == bodega.Id);

            // Actualiza cada propiedad por los datos nuevos pasados en parametro
            if(bodegaBD != null)
            {
                bodegaBD.Nombre = bodega.Nombre;
                bodegaBD.Descripcion = bodega.Descripcion;
                bodegaBD.Estado = bodega.Estado;
                _db.SaveChanges();
            }
        }
    }
}
