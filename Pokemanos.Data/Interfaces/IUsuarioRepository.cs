using Pokemanos.Model;
using System.Threading.Tasks;

namespace Pokemanos.Data.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> GetByEmail(string email);
    }
}
