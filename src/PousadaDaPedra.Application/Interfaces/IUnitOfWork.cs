namespace PousadaDaPedra.Application.Interfaces;

public interface IUnitOfWork
{
    Task Commit();
}