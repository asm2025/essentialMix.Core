﻿using System.Threading;
using System.Threading.Tasks;
using essentialMix.Data.Model;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SystemDbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace essentialMix.Core.Data.Entity.Patterns.Repository;

public abstract class Repository<TContext, TEntity, TKey> : RepositoryBase<TContext, TEntity, TKey>, IRepository<TContext, TEntity, TKey>
	where TContext : SystemDbContext
	where TEntity : class, IEntity
{
	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration)
		: this(context, configuration, null, false)
	{
	}

	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration, ILogger logger)
		: this(context, configuration, logger, false)
	{
	}

	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration, ILogger logger, bool ownsContext)
		: base(context, configuration, logger, ownsContext)
	{
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Add(TEntity entity)
	{
		ThrowIfDisposed();
		return AddInternal(entity);
	}

	[NotNull]
	protected virtual TEntity AddInternal([NotNull] TEntity entity) { return DbSet.Add(entity).Entity; }

	/// <inheritdoc />
	public ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return AddAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> AddAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(AddInternal(entity));
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Attach(TEntity entity)
	{
		ThrowIfDisposed();
		return AttachInternal(entity);
	}

	[NotNull]
	protected virtual TEntity AttachInternal([NotNull] TEntity entity) { return DbSet.Attach(entity).Entity; }

	/// <inheritdoc />
	public ValueTask<TEntity> AttachAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return AttachAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> AttachAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(AttachInternal(entity));
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Update(TEntity entity)
	{
		ThrowIfDisposed();
		return UpdateInternal(entity);
	}

	[NotNull]
	protected virtual TEntity UpdateInternal([NotNull] TEntity entity)
	{
		DbSet.Attach(entity);
		Context.Entry(entity).State = EntityState.Modified;
		return entity;
	}

	/// <inheritdoc />
	public ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return UpdateAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> UpdateAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(UpdateInternal(entity));
	}

	/// <inheritdoc />
	public TEntity Delete(TKey key)
	{
		ThrowIfDisposed();
		return DeleteInternal(key);
	}

	protected virtual TEntity DeleteInternal([NotNull] TKey key)
	{
		TEntity entity = DbSet.Find(key);
		return entity == null
					? null
					: DeleteInternal(entity);
	}

	/// <inheritdoc />
	public ValueTask<TEntity> DeleteAsync(TKey key, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return DeleteAsyncInternal(key, token);
	}

	protected virtual async ValueTask<TEntity> DeleteAsyncInternal([NotNull] TKey key, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		TEntity entity = await DbSet.FindAsync(new object[] { key }, token);
		if (entity == null) return null;
		token.ThrowIfCancellationRequested();
		return await DeleteAsyncInternal(entity, token);
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Delete(TEntity entity)
	{
		ThrowIfDisposed();
		return DeleteInternal(entity);
	}

	[NotNull]
	protected virtual TEntity DeleteInternal([NotNull] TEntity entity)
	{
		if (Context.Entry(entity).State == EntityState.Detached) DbSet.Attach(entity);
		return DbSet.Remove(entity).Entity;
	}

	public ValueTask<TEntity> DeleteAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return DeleteAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> DeleteAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(DeleteInternal(entity));
	}
}

public abstract class Repository<TContext, TEntity, TKey1, TKey2> : RepositoryBase<TContext, TEntity, TKey1, TKey2>, IRepository<TContext, TEntity, TKey1, TKey2>
	where TContext : SystemDbContext
	where TEntity : class, IEntity
{
	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration)
		: this(context, configuration, null, false)
	{
	}

	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration, ILogger logger)
		: this(context, configuration, logger, false)
	{
	}

	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration, ILogger logger, bool ownsContext)
		: base(context, configuration, logger, ownsContext)
	{
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Add(TEntity entity)
	{
		ThrowIfDisposed();
		return AddInternal(entity);
	}

	[NotNull]
	protected virtual TEntity AddInternal([NotNull] TEntity entity) { return DbSet.Add(entity).Entity; }

	/// <inheritdoc />
	public ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return AddAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> AddAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(AddInternal(entity));
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Attach(TEntity entity)
	{
		ThrowIfDisposed();
		return AttachInternal(entity);
	}

	[NotNull]
	protected virtual TEntity AttachInternal([NotNull] TEntity entity) { return DbSet.Attach(entity).Entity; }

	/// <inheritdoc />
	public ValueTask<TEntity> AttachAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return AttachAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> AttachAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(AttachInternal(entity));
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Update(TEntity entity)
	{
		ThrowIfDisposed();
		return UpdateInternal(entity);
	}

	[NotNull]
	protected virtual TEntity UpdateInternal([NotNull] TEntity entity)
	{
		DbSet.Attach(entity);
		Context.Entry(entity).State = EntityState.Modified;
		return entity;
	}

	/// <inheritdoc />
	public ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return UpdateAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> UpdateAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(UpdateInternal(entity));
	}

	/// <inheritdoc />
	public TEntity Delete(TKey1 key1, TKey2 key2)
	{
		ThrowIfDisposed();
		return DeleteInternal(key1, key2);
	}

	protected virtual TEntity DeleteInternal([NotNull] TKey1 key1, TKey2 key2)
	{
		TEntity entity = DbSet.Find(key1, key2);
		return entity == null
					? null
					: DeleteInternal(entity);
	}

	/// <inheritdoc />
	public ValueTask<TEntity> DeleteAsync(TKey1 key1, TKey2 key2, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return DeleteAsyncInternal(key1, key2, token);
	}

	protected virtual async ValueTask<TEntity> DeleteAsyncInternal([NotNull] TKey1 key1, TKey2 key2, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		TEntity entity = await DbSet.FindAsync(new object[] { key1, key2 }, token);
		if (entity == null) return null;
		token.ThrowIfCancellationRequested();
		return await DeleteAsyncInternal(entity, token);
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Delete(TEntity entity)
	{
		ThrowIfDisposed();
		return DeleteInternal(entity);
	}

	[NotNull]
	protected virtual TEntity DeleteInternal([NotNull] TEntity entity)
	{
		if (Context.Entry(entity).State == EntityState.Detached) DbSet.Attach(entity);
		return DbSet.Remove(entity).Entity;
	}

	public ValueTask<TEntity> DeleteAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return DeleteAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> DeleteAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(DeleteInternal(entity));
	}
}

public abstract class Repository<TContext, TEntity, TKey1, TKey2, TKey3> : RepositoryBase<TContext, TEntity, TKey1, TKey2, TKey3>, IRepository<TContext, TEntity, TKey1, TKey2, TKey3>
	where TContext : SystemDbContext
	where TEntity : class, IEntity
{
	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration)
		: this(context, configuration, null, false)
	{
	}

	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration, ILogger logger)
		: this(context, configuration, logger, false)
	{
	}

	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration, ILogger logger, bool ownsContext)
		: base(context, configuration, logger, ownsContext)
	{
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Add(TEntity entity)
	{
		ThrowIfDisposed();
		return AddInternal(entity);
	}

	[NotNull]
	protected virtual TEntity AddInternal([NotNull] TEntity entity) { return DbSet.Add(entity).Entity; }

	/// <inheritdoc />
	public ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return AddAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> AddAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(AddInternal(entity));
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Attach(TEntity entity)
	{
		ThrowIfDisposed();
		return AttachInternal(entity);
	}

	[NotNull]
	protected virtual TEntity AttachInternal([NotNull] TEntity entity) { return DbSet.Attach(entity).Entity; }

	/// <inheritdoc />
	public ValueTask<TEntity> AttachAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return AttachAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> AttachAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(AttachInternal(entity));
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Update(TEntity entity)
	{
		ThrowIfDisposed();
		return UpdateInternal(entity);
	}

	[NotNull]
	protected virtual TEntity UpdateInternal([NotNull] TEntity entity)
	{
		DbSet.Attach(entity);
		Context.Entry(entity).State = EntityState.Modified;
		return entity;
	}

	/// <inheritdoc />
	public ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return UpdateAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> UpdateAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(UpdateInternal(entity));
	}

	/// <inheritdoc />
	public TEntity Delete(TKey1 key1, TKey2 key2, TKey3 key3)
	{
		ThrowIfDisposed();
		return DeleteInternal(key1, key2, key3);
	}

	protected virtual TEntity DeleteInternal([NotNull] TKey1 key1, TKey2 key2, TKey3 key3)
	{
		TEntity entity = DbSet.Find(key1, key2, key3);
		return entity == null
					? null
					: DeleteInternal(entity);
	}

	/// <inheritdoc />
	public ValueTask<TEntity> DeleteAsync(TKey1 key1, TKey2 key2, TKey3 key3, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return DeleteAsyncInternal(key1, key2, key3, token);
	}

	protected virtual async ValueTask<TEntity> DeleteAsyncInternal([NotNull] TKey1 key1, TKey2 key2, TKey3 key3, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		TEntity entity = await DbSet.FindAsync(new object[] { key1, key2, key3 }, token);
		if (entity == null) return null;
		token.ThrowIfCancellationRequested();
		return await DeleteAsyncInternal(entity, token);
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Delete(TEntity entity)
	{
		ThrowIfDisposed();
		return DeleteInternal(entity);
	}

	[NotNull]
	protected virtual TEntity DeleteInternal([NotNull] TEntity entity)
	{
		if (Context.Entry(entity).State == EntityState.Detached) DbSet.Attach(entity);
		return DbSet.Remove(entity).Entity;
	}

	public ValueTask<TEntity> DeleteAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return DeleteAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> DeleteAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(DeleteInternal(entity));
	}
}

public abstract class Repository<TContext, TEntity, TKey1, TKey2, TKey3, TKey4> : RepositoryBase<TContext, TEntity, TKey1, TKey2, TKey3, TKey4>, IRepository<TContext, TEntity, TKey1, TKey2, TKey3, TKey4>
	where TContext : SystemDbContext
	where TEntity : class, IEntity
{
	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration)
		: this(context, configuration, null, false)
	{
	}

	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration, ILogger logger)
		: this(context, configuration, logger, false)
	{
	}

	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration, ILogger logger, bool ownsContext)
		: base(context, configuration, logger, ownsContext)
	{
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Add(TEntity entity)
	{
		ThrowIfDisposed();
		return AddInternal(entity);
	}

	[NotNull]
	protected virtual TEntity AddInternal([NotNull] TEntity entity) { return DbSet.Add(entity).Entity; }

	/// <inheritdoc />
	public ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return AddAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> AddAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(AddInternal(entity));
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Attach(TEntity entity)
	{
		ThrowIfDisposed();
		return AttachInternal(entity);
	}

	[NotNull]
	protected virtual TEntity AttachInternal([NotNull] TEntity entity) { return DbSet.Attach(entity).Entity; }

	/// <inheritdoc />
	public ValueTask<TEntity> AttachAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return AttachAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> AttachAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(AttachInternal(entity));
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Update(TEntity entity)
	{
		ThrowIfDisposed();
		return UpdateInternal(entity);
	}

	[NotNull]
	protected virtual TEntity UpdateInternal([NotNull] TEntity entity)
	{
		DbSet.Attach(entity);
		Context.Entry(entity).State = EntityState.Modified;
		return entity;
	}

	/// <inheritdoc />
	public ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return UpdateAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> UpdateAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(UpdateInternal(entity));
	}

	/// <inheritdoc />
	public TEntity Delete(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4)
	{
		ThrowIfDisposed();
		return DeleteInternal(key1, key2, key3, key4);
	}

	protected virtual TEntity DeleteInternal([NotNull] TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4)
	{
		TEntity entity = DbSet.Find(key1, key2, key3, key4);
		return entity == null
					? null
					: DeleteInternal(entity);
	}

	/// <inheritdoc />
	public ValueTask<TEntity> DeleteAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return DeleteAsyncInternal(key1, key2, key3, key4, token);
	}

	protected virtual async ValueTask<TEntity> DeleteAsyncInternal([NotNull] TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		TEntity entity = await DbSet.FindAsync(new object[]
		{
			key1,
			key2,
			key3,
			key4
		}, token);
		if (entity == null) return null;
		token.ThrowIfCancellationRequested();
		return await DeleteAsyncInternal(entity, token);
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Delete(TEntity entity)
	{
		ThrowIfDisposed();
		return DeleteInternal(entity);
	}

	[NotNull]
	protected virtual TEntity DeleteInternal([NotNull] TEntity entity)
	{
		if (Context.Entry(entity).State == EntityState.Detached) DbSet.Attach(entity);
		return DbSet.Remove(entity).Entity;
	}

	public ValueTask<TEntity> DeleteAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return DeleteAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> DeleteAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(DeleteInternal(entity));
	}
}

public abstract class Repository<TContext, TEntity, TKey1, TKey2, TKey3, TKey4, TKey5> : RepositoryBase<TContext, TEntity, TKey1, TKey2, TKey3, TKey4, TKey5>, IRepository<TContext, TEntity, TKey1, TKey2, TKey3, TKey4, TKey5>
	where TContext : SystemDbContext
	where TEntity : class, IEntity
{
	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration)
		: this(context, configuration, null, false)
	{
	}

	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration, ILogger logger)
		: this(context, configuration, logger, false)
	{
	}

	/// <inheritdoc />
	protected Repository([NotNull] TContext context, [NotNull] IConfiguration configuration, ILogger logger, bool ownsContext)
		: base(context, configuration, logger, ownsContext)
	{
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Add(TEntity entity)
	{
		ThrowIfDisposed();
		return AddInternal(entity);
	}

	[NotNull]
	protected virtual TEntity AddInternal([NotNull] TEntity entity) { return DbSet.Add(entity).Entity; }

	/// <inheritdoc />
	public ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return AddAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> AddAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(AddInternal(entity));
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Attach(TEntity entity)
	{
		ThrowIfDisposed();
		return AttachInternal(entity);
	}

	[NotNull]
	protected virtual TEntity AttachInternal([NotNull] TEntity entity) { return DbSet.Attach(entity).Entity; }

	/// <inheritdoc />
	public ValueTask<TEntity> AttachAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return AttachAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> AttachAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(AttachInternal(entity));
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Update(TEntity entity)
	{
		ThrowIfDisposed();
		return UpdateInternal(entity);
	}

	[NotNull]
	protected virtual TEntity UpdateInternal([NotNull] TEntity entity)
	{
		DbSet.Attach(entity);
		Context.Entry(entity).State = EntityState.Modified;
		return entity;
	}

	/// <inheritdoc />
	public ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return UpdateAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> UpdateAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(UpdateInternal(entity));
	}

	/// <inheritdoc />
	public TEntity Delete(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5)
	{
		ThrowIfDisposed();
		return DeleteInternal(key1, key2, key3, key4, key5);
	}

	protected virtual TEntity DeleteInternal([NotNull] TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5)
	{
		TEntity entity = DbSet.Find(key1, key2, key3, key4, key5);
		return entity == null
					? null
					: DeleteInternal(entity);
	}

	/// <inheritdoc />
	public ValueTask<TEntity> DeleteAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return DeleteAsyncInternal(key1, key2, key3, key4, key5, token);
	}

	protected virtual async ValueTask<TEntity> DeleteAsyncInternal([NotNull] TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		TEntity entity = await DbSet.FindAsync(new object[]
		{
			key1,
			key2,
			key3,
			key4,
			key5
		}, token);
		if (entity == null) return null;
		token.ThrowIfCancellationRequested();
		return await DeleteAsyncInternal(entity, token);
	}

	/// <inheritdoc />
	[NotNull]
	public TEntity Delete(TEntity entity)
	{
		ThrowIfDisposed();
		return DeleteInternal(entity);
	}

	[NotNull]
	protected virtual TEntity DeleteInternal([NotNull] TEntity entity)
	{
		if (Context.Entry(entity).State == EntityState.Detached) DbSet.Attach(entity);
		return DbSet.Remove(entity).Entity;
	}

	public ValueTask<TEntity> DeleteAsync(TEntity entity, CancellationToken token = default(CancellationToken))
	{
		ThrowIfDisposed();
		token.ThrowIfCancellationRequested();
		return DeleteAsyncInternal(entity, token);
	}

	protected virtual ValueTask<TEntity> DeleteAsyncInternal([NotNull] TEntity entity, CancellationToken token = default(CancellationToken))
	{
		token.ThrowIfCancellationRequested();
		return new ValueTask<TEntity>(DeleteInternal(entity));
	}
}
