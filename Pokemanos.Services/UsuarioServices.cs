using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.Extensions.Logging;
using Pokemanos.Data.Interfaces;
using Pokemanos.Model;
using Pokemanos.Services.Interfaces;

namespace Pokemanos.Services
{
    public class UsuarioServices : IUsuarioServices
    {


        #region repositorios
        private readonly ILogger<UsuarioServices> _logger;

        public IUsuarioRepository UsuarioRepository { get; set; }
        #endregion

        public UsuarioServices(
            IUsuarioRepository usuarioRepository,
            ILogger<UsuarioServices> logger
            )
        {
            UsuarioRepository = usuarioRepository;
            _logger = logger;
        }


        public Usuario Save(Usuario usuario, int usuarioId = 0)
        {
            try
            {

                if (usuario.Id == 0)
                {
                    CadastroUsuario(usuario);

                    //todo ENVIAR EMAIL DE BOAS VINDAS
                }
                else
                {
                    AtualizarUsuario(usuario, usuarioId);
                }

                return UsuarioRepository.FindFirstBy(x => x.Email == usuario.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public void CadastroUsuario(Usuario usuario)
        {
            if (UsuarioRepository.FindFirstBy(x => x.Email == usuario.Email) != null) throw new ValidationException("E-mail já cadastrado!");

            usuario.DataCadastro = DateTime.Now;
            usuario.Senha = usuario.Senha.Md5Hash();

            UsuarioRepository.Add(usuario);
            UsuarioRepository.SaveChanges();
        }


        public void AtualizarUsuario(Usuario usuarioNovo, int usuarioId)
        {
            if (usuarioNovo.Id != usuarioId) throw new Exception("Acesso negado!");

            var usuario = UsuarioRepository.GetById(usuarioNovo.Id);
            usuario.Nome = usuarioNovo.Nome;
            usuario.Email = usuarioNovo.Email;
            usuario.Telefone = usuarioNovo.Telefone;

            UsuarioRepository.Update(usuario);
            UsuarioRepository.SaveChanges();
        }

    }
}