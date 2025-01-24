using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Persistence;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private Hashtable? _repositories;
        private readonly EcommerceDbContext _context;

        public UnitOfWork(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception("Error en transacción", e);
            }

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null) //si no hay un hashtable inicializado
            {
                _repositories = new Hashtable(); //lo inicializamos con un nuevo HashTable
            }

            var type = typeof(TEntity).Name; //nombre de la clase entidad

            if (!_repositories.ContainsKey(type)) //Si dentro de la collección de entidades de repositorios no está el tipo declarado en type entonces...
            {
                var repositoryType = typeof(RepositoryBase<>); //obtiene el tipo del repositorio
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context); //crea una instancia generica utilizando el type TEntity
                _repositories.Add(type, repositoryInstance); //Lo agrega al Hashtable
            }

            return (IAsyncRepository<TEntity>)_repositories[type]!; //devuelve el repositorio especifico agregandole la entidad concreta
        }
    }
}