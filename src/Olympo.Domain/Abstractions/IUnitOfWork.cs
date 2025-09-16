namespace Olympo.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<Guid> BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(Guid transactionId, CancellationToken cancellationToken);
    Task RollbackTransactionAsync(Guid transactionId, CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
