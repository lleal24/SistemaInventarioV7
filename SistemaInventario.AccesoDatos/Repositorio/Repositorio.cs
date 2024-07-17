using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos.Especificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio.IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
                _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task Agregar(T entidad)
        {
            // Insert into Table
            await dbSet.AddAsync(entidad); 
        }

        public async Task<T> Obtener(int id)
        {
            // select * from (Solo por el Id)
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if(filtro != null)
            {
                // select * from where ... propiedad o valor enviado
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    // ejemplo "Categoria,Marca"
                    query = query.Include(incluirProp);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!isTracking)
            {
                // Devuelve una nueva consulta en la que las entidades devueltas
                // no se almacenarán en caché en o DbContextObjectContext
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public PagedList<T> ObtenerTodosPaginado(Parametros parametros, Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                // select * from where ... propiedad o valor enviado
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // ejemplo "Categoria,Marca"
                    query = query.Include(incluirProp);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!isTracking)
            {
                // Devuelve una nueva consulta en la que las entidades devueltas
                // no se almacenarán en caché en o DbContextObjectContext
                query = query.AsNoTracking();
            }
            return PagedList<T>.ToPagedList(query, parametros.PageNumber, parametros.PageSize);

        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                // select * from where ... propiedad o valor enviado
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // ejemplo "Categoria,Marca"
                    query = query.Include(incluirProp);
                }
            }
            if (!isTracking)
            {
                // Devuelve una nueva consulta en la que las entidades devueltas
                // no se almacenarán en caché en o DbContextObjectContext
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public void Remover(T entidad)
        {
            // Delete table
            dbSet.Remove(entidad);
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);
        }

       
    }
}
