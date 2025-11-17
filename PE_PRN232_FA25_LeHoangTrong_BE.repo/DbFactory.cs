using System;
using Microsoft.EntityFrameworkCore;

namespace PE_PRN232_FA25_LeHoangTrong_BE_repo;

public interface IDbFactory<TContext>
    where TContext : DbContext
{
    TContext DbContext { get; }
}

public sealed class DbFactory<TContext> : IDbFactory<TContext>
    where TContext : DbContext
{
    private readonly Func<TContext> _factory;
    private TContext? _dbContext;

    public DbFactory(Func<TContext> factory)
    {
        _factory = factory;
    }

    public TContext DbContext => _dbContext ??= _factory();
}