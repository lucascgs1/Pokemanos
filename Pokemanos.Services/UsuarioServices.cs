using System;
using System.Collections.Generic;
using System.Text;
using Pokemanos.Data.Interfaces;
using Pokemanos.Model;
using Pokemanos.Services.Interfaces;

namespace Pokemanos.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        #region repositorios
        public IUsuarioRepository UsuarioRepository { get; set; }
        #endregion

        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {
            UsuarioRepository = usuarioRepository;
        }
    }
}