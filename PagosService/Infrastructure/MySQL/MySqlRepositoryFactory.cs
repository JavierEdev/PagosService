using PagosService.Application.Interfaces;
using PagosService.Infrastructure.Data;

namespace PagosService.Infrastructure.MySQL
{
    public class MySqlRepositoryFactory : IRepositoryFactory
    {
        private readonly MyDbContext _context;
        private readonly IMongoLogger _logger;

        public MySqlRepositoryFactory(MyDbContext context, IMongoLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public IPagoRepository CreatePagoRepository()
        {
            return new PagoMySqlAdapter(_context, _logger);
        }
    }
}
