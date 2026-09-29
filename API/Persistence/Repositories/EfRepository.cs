using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Persistence.Repositories
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, ICompanyEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        private static readonly PropertyInfo? _isActiveProp = typeof(TEntity).GetProperty("IsActive", BindingFlags.Public | BindingFlags.Instance);

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(int companyId, bool includeInactive = false)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            query = query.Where(x => x.CompanyId == companyId);

            if (!includeInactive && HasIsActive())
                query = query.Where(x => EF.Property<bool?>(x, "IsActive") == true);

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity?> FindByIdAsync(int id, int companyId, bool includeInactive = false)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            query = query.Where(x => x.Id == id && x.CompanyId == companyId);

            if (!includeInactive && HasIsActive())
                query = query.Where(x => EF.Property<bool?>(x, "IsActive") == true);

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> SaveAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                if (entity.Id == 0)
                    await _dbSet.AddAsync(entity);
                else
                    _dbSet.Update(entity);
            }

            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int id, int companyId)
        {
            var entity = await FindByIdAsync(id, companyId);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        protected static bool HasIsActive()
        {
            if (_isActiveProp == null)
                return false;

            var t = _isActiveProp.PropertyType;

            return t == typeof(bool) || t == typeof(bool?);
        }
    }
}
