using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ExampleProject.Command.Helpers.Fetchers
{
    public interface IEntityGetter<TEntity> where TEntity : class
    {
        TEntity Get(
            Expression<Func<TEntity, bool>> expression,
            string includeProperties = "");

        TEntity GetById(Guid id, string includeProperties = "");

        IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
    }

    public class EntityGetter<TEntity> : IEntityGetter<TEntity>
        where TEntity : class
    {
        private readonly ExampleProjectCommandContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public EntityGetter(ExampleProjectCommandContext context)
        {
            if (context == null)
                throw new ArgumentException("dbContext cannot be null");

            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual TEntity Get(
         Expression<Func<TEntity, bool>> expression,
             string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (expression != null)
                query = query.Where(expression);

            query = includeProperties
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.SingleOrDefault();
        }

        public virtual TEntity GetById(Guid id, string includeProperties = "")
        {
            Func<dynamic, bool> getById = x => x.Id == id;

            IQueryable<dynamic> query = _dbSet;

            query = includeProperties
              .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
              .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.SingleOrDefault(getById);
        }

        public IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (expression != null)
                query = query.Where(expression);

            query = includeProperties
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
                return orderBy(query);

            return query;
        }
    }
}
