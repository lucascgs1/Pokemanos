using Pokemanos.Model;

namespace Pokemanos.Data.Interfaces
{
    public interface ICodigoSegurancaRepository : IRepository<CodigoSeguranca>
    {
        CodigoSeguranca GetByUserId(int userId);

        void DeleteByUserId(int userId);
    }
}
