namespace Xpto.Infra.Database.Relational;

public interface IUnitOfWork
{
    Task<bool> Commit();
}