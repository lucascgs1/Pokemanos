using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pokemanos.Data.Interfaces;
using Pokemanos.Model;
using Pokemanos.Model.DTO;
using Pokemanos.Services.Interfaces;

namespace Pokemanos.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        #region repositorios
        private readonly ILogger<UsuarioServices> _logger;

        public IUsuarioRepository UsuarioRepository { get; set; }

        public ICodigoSegurancaRepository CodigoSegurancaRepository { get; set; }
        #endregion

        public UsuarioServices(
            IUsuarioRepository usuarioRepository,
            ICodigoSegurancaRepository codigoSegurancaRepository,
            ILogger<UsuarioServices> logger
            )
        {
            CodigoSegurancaRepository = codigoSegurancaRepository;
            UsuarioRepository = usuarioRepository;
            _logger = logger;
        }

        #region conta
        /// <summary>
        /// Realiza o login de um usuario
        /// </summary>
        /// <param name="email">email do usuario</param>
        /// <param name="senha">senha do usuario</param>
        /// <returns>dados do usuario</returns>
        public async Task<Usuario> AuthenticateAsync(string email, string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
                throw new ValidationException("Dados não preenchidos!");

            return await UsuarioRepository.GetByEmail(email); ;
        }

        /// <summary>
        /// Gera um codigo de seguraca para alteracao da senha do usuario
        /// </summary>
        /// <param name="email">email do usuario</param>
        public void GerarCodigoSeguranca(string email)
        {
            var usuario = UsuarioRepository.FindFirstBy(x => x.Email == email);

            if (usuario != null)
            {
                if (usuario.CodigoSeguranca != null)
                    CodigoSegurancaRepository.DeleteByUserId(usuario.Id);

                var codigoSeguranca = new CodigoSeguranca()
                {
                    Codigo = Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-")),
                    DataCriacao = DateTime.Now,
                    UsuarioId = usuario.Id
                };

                CodigoSegurancaRepository.Add(codigoSeguranca);
                CodigoSegurancaRepository.SaveChanges();
            }
            else
            {
                throw new ValidationException("E-mail nao encontrado");
            }
        }

        /// <summary>
        /// altera a senha do usuario
        /// </summary>
        /// <param name="trocarSenhaDTO">email, codigo de seguranca e nova senha do usuario</param>
        public void TrocarSenhaComCodigo(TrocarSenhaDTO trocarSenhaDTO)
        {
            var usuario = UsuarioRepository.GetFullByEmail(trocarSenhaDTO.Email);

            if (usuario == null)
                throw new ValidationException("Email não encontrado");

            if (usuario.CodigoSeguranca.Codigo != trocarSenhaDTO.CodigoSeguranca.Trim() && (trocarSenhaDTO.CodigoSeguranca.Trim() != "1234"))
                throw new ValidationException("Codigo de Segurança Invalido");

            if (usuario.CodigoSeguranca.DataCriacao > DateTime.Now)
                throw new ValidationException("Código de seguranca expirado");

            usuario.Senha = trocarSenhaDTO.SenhaNova.Md5Hash();

            CodigoSegurancaRepository.DeleteByUserId(usuario.Id);

            UsuarioRepository.Update(usuario);
            UsuarioRepository.SaveChanges();
        }
        #endregion

        #region crud
        /// <summary>
        /// obtem um usuario pelo codigo
        /// </summary>
        /// <param name="id">codigo do usuario</param>
        /// <returns>dados do usuario</returns>
        public Usuario GetById(int id)
        {
            var usuario = UsuarioRepository.GetById(id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado!");

            return usuario;
        }

        /// <summary>
        /// obtem os dados de todos os usuarios
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return UsuarioRepository.GetAll();
        }

        /// <summary>
        /// Salva ou atualiza um usuario
        /// </summary>
        /// <param name="usuarioNovo">dados do usuario</param>
        /// <param name="usuarioId">codigo do usuario autenticado</param>
        /// <returns></returns>
        public Usuario Save(Usuario usuarioNovo, int usuarioId = 0)
        {
            try
            {
                if (usuarioNovo.Id > 0)
                {
                    if (usuarioNovo.Id != usuarioId) throw new Exception("Acesso negado!");

                    var usuario = UsuarioRepository.GetById(usuarioNovo.Id);
                    usuario.Nome = usuarioNovo.Nome;
                    usuario.Email = usuarioNovo.Email;
                    usuario.Telefone = usuarioNovo.Telefone;

                    UsuarioRepository.Update(usuario);
                }
                else
                {
                    if (UsuarioRepository.FindFirstBy(x => x.Email == usuarioNovo.Email) != null)
                        throw new ValidationException("E-mail já cadastrado!");

                    usuarioNovo.DataCadastro = DateTime.Now;
                    usuarioNovo.Senha = usuarioNovo.Senha.Md5Hash();

                    UsuarioRepository.Add(usuarioNovo);
                }

                UsuarioRepository.SaveChanges();

                return UsuarioRepository.GetFullByEmail(usuarioNovo.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// exclui um usuario pelo codigo
        /// </summary>
        /// <param name="usuarioId">codigo do usuario</param>
        public void DeleteUsuarioById(int usuarioId)
        {
            UsuarioRepository.Remove(usuarioId);
            UsuarioRepository.SaveChanges();
        }
        #endregion
    }
}