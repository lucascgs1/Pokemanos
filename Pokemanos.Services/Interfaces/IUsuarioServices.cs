using System.Collections.Generic;
using System.Threading.Tasks;
using Pokemanos.Model;
using Pokemanos.Model.DTO;

namespace Pokemanos.Services.Interfaces
{
    public interface IUsuarioServices
    {
        #region conta
        /// <summary>
        /// Realiza o login de um usuario
        /// </summary>
        /// <param name="email">email do usuario</param>
        /// <param name="senha">senha do usuario</param>
        /// <returns>dados do usuario</returns>
        Task<Usuario> AuthenticateAsync(string email, string senha);

        /// <summary>
        /// Gera um codigo de seguraca para alteracao da senha do usuario
        /// </summary>
        /// <param name="email">email do usuario</param>
        void GerarCodigoSeguranca(string email);

        /// <summary>
        /// altera a senha do usuario
        /// </summary>
        /// <param name="trocarSenhaDTO">email, codigo de seguranca e nova senha do usuario</param>
        void TrocarSenhaComCodigo(TrocarSenhaDTO trocarSenhaDTO);
        #endregion

        #region crud
        /// <summary>
        /// obtem um usuario pelo codigo
        /// </summary>
        /// <param name="id">codigo do usuario</param>
        /// <returns>dados do usuario</returns>
        Usuario GetById(int id);

        /// <summary>
        /// obtem os dados de todos os usuarios
        /// </summary>
        /// <returns></returns>
        IEnumerable<Usuario> GetAllUsuarios();

        /// <summary>
        /// Salva ou atualiza um usuario
        /// </summary>
        /// <param name="usuarioNovo">dados do usuario</param>
        /// <param name="usuarioId">codigo do usuario autenticado</param>
        /// <returns></returns>
        Usuario Save(Usuario usuario, int usuarioId = 0);

        /// <summary>
        /// exclui um usuario pelo codigo
        /// </summary>
        /// <param name="usuarioId">codigo do usuario</param>
        void DeleteUsuarioById(int usuarioId);
        #endregion
    }
}
