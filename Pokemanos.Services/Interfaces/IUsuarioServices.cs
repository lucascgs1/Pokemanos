using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pokemanos.Model;

namespace Pokemanos.Services.Interfaces
{
    public interface IUsuarioServices
    {
        #region autenticacao
        Task<Usuario> AuthenticateAsync(string email, string senha);
        #endregion


        #region usuario
        Usuario Save(Usuario usuario, int usuarioId = 0);

        Usuario GetById(int id);
        #endregion
    }
}
