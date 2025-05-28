namespace PagosService.Application.Interfaces
{
    public interface IRepositoryFactory
    {
        IPagoRepository CreatePagoRepository();
    }
}
