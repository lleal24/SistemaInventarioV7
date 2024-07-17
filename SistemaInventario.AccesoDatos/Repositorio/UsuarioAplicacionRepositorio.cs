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
    public class UsuarioAplicacionRepositorio : Repositorio<UsuarioAplicacion>, IRepositorio.IUsuarioAplicacionRepositorio
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Para pasar al padre : base(db)
        /// </summary>
        /// <param name="db"></param>
        public UsuarioAplicacionRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
