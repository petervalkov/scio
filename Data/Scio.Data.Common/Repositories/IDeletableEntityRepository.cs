﻿namespace Scio.Data.Common.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Scio.Data.Common.Models;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        IQueryable<TEntity> AllWithDeletedIncluding(string collection);

        Task<TEntity> GetByIdWithDeletedAsync(params object[] id);

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);

        Task<TEntity> FindByIdAsync(params object[] id);
    }
}
