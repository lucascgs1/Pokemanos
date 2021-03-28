using System;
using System.Collections.Generic;
using System.Text;
using Pokemanos.Model;

namespace Pokemanos.Services.Interfaces
{
    public interface IUsuarioServices
    {
        Usuario Save(Usuario usuario, int usuarioId = 0);
    }
}
