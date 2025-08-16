using EducationApp.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using EducationApp.Core.Entities;

namespace EducationApp.Application.Repositories.Repository;

public abstract class Repository<TEntity>(EduDbContext context) : 
    IRepository<TEntity> where TEntity : class
{
    public void Add(TEntity entity)
        => context.Set<TEntity>().Add(entity);

    public async Task AddAsync(TEntity entity)
        => await context.Set<TEntity>().AddAsync(entity);

    public void Update(TEntity entity)
        => context.Entry(entity).State = EntityState.Modified;

    public void Delete(TEntity entity)
        => context.Set<TEntity>().Remove(entity);

    public IQueryable<TEntity> GetAll()
        => context.Set<TEntity>().AsNoTracking();

    public TEntity? GetById(int id)
        => context.Set<TEntity>().Find(id);

    public async Task<TEntity?> GetByIdAsync(int id)
        => await context.Set<TEntity>().FindAsync(id);

    public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> predicate)
        => context.Set<TEntity>().AsNoTracking().Where(predicate);

    public int SaveChanges()
        => context.SaveChanges();

    public async Task<int> SaveChangesAsync()
        => await context.SaveChangesAsync();
}
