﻿using essentialMix.Core.Data.Entity.Patterns.Repository;
using essentialMix.Data.Model;
using essentialMix.Data.Patterns.Service;
using SystemDbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace essentialMix.Core.Data.Entity.Patterns.Service;

public interface IService<TContext, TRepository, TEntity, TKey> : IServiceBase<TContext, TRepository, TEntity, TKey>, IService<TEntity, TKey>
	where TContext : SystemDbContext
	where TRepository : IRepository<TContext, TEntity, TKey>
	where TEntity : class, IEntity
{
}