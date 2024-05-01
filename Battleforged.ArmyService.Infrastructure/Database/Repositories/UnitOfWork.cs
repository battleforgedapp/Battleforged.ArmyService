using Battleforged.ArmyService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories;

/// <inheritdoc cref="IUnitOfWork" />
public sealed class UnitOfWork(IDbContextFactory<AppDbContext> ctx) : IUnitOfWork, IDisposable {
    
    private IDbContextTransaction? _transaction;
    private readonly AppDbContext _ctx = ctx.CreateDbContext();

    public void Dispose() {
        _transaction?.Dispose();
        _ctx?.Dispose();
    }

    /// <inheritdoc cref="IUnitOfWork.BeginTransactionAsync" />
    public async Task BeginTransactionAsync(CancellationToken ct = default) {
        _transaction = await _ctx.Database.BeginTransactionAsync(ct);
    }

    /// <inheritdoc cref="IUnitOfWork.CommitAsync" />
    public async Task CommitAsync(CancellationToken ct = default) {
        await _transaction?.CommitAsync(ct)!;
    }

    /// <inheritdoc cref="IUnitOfWork.RollbackAsync" />
    public async Task RollbackAsync(CancellationToken ct = default) {
        await _transaction?.RollbackAsync(ct)!;
    }
}