using Microsoft.EntityFrameworkCore;
using Pokemanos.Data.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Pokemanos.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ApplicationDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public TEntity FindFirstBy(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = DbSet.Where(predicate).AsQueryable();
            return query;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable<TEntity>();
        }


        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(int id)
        {
            DbSet.Remove(DbSet.Find(id));

            Db.SaveChanges();
        }

        public virtual void RemoveRange(IEnumerable<TEntity> obj)
        {
            DbSet.RemoveRange(obj);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            Db.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Db.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            Db.Database.RollbackTransaction();
        }
    }
}
