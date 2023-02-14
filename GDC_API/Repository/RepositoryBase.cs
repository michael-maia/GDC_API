using GDC_API.Data;
using GDC_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GDC_API.Repository
{
    // Este é um repositório genérico do qual vai nos fornecer todos os métodos CRUD com o banco de dados
    // Uso da palavra chave 'abstract' -> https://docs.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/abstract
    // <T> -> Significa que não é necessário mencionar exatamente o modelo (classe)
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MySQLDatabaseContext RepositoryContext { get; set; }

        // Injeta a dependência com o banco de dados através do contexto de MySQL
        public RepositoryBase(MySQLDatabaseContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll() => RepositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    }
}
