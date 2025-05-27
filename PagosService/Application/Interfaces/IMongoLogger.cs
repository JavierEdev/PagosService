using System.Collections.Generic;
using System.Threading.Tasks;

namespace PagosService.Application.Interfaces
{
    public interface IMongoLogger
    {
        Task RegistrarLogAsync(string nivel, string mensaje, Dictionary<string, object>? contexto = null);
    }
}
