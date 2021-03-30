using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pokemanos.Api.Model;
using Pokemanos.Model;
using Pokemanos.Services.Interfaces;
using Pokemones.API.Helper;
using System;
using System.Linq;
using System.Security.Claims;

namespace Pokemanos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(
            ILogger<UsuarioController> logger,
            IOptions<AppSettings> appSettings
            )
        {
            this._logger = logger;
            this.appSettings = appSettings;
        }

        /// <summary>
        /// Obtem um usuario pelo id
        /// </summary>
        /// <param name="usuarioServices">servico de usuario</param>
        /// <param name="id">codigo do usuario</param>
        /// <returns>dados do usuario</returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetById([FromServices] IUsuarioServices usuarioServices, int id)
        {
            try
            {
                var usuario = usuarioServices.GetById(id);

                if (usuario == null)
                    return NotFound("Usuário não encontrado!");

                return Ok(new { usuario });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// retorna todos os usuarios cadastrados
        /// </summary>
        /// <param name="usuarioServices">servico de usuario</param>
        /// <returns>lista de usuarios</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAll([FromServices] IUsuarioServices usuarioServices)
        {
            try
            {
                var usuarios = usuarioServices.GetAllUsuarios().ToList();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Problem(ex.Message);
            }
        }
        
        /// <summary>
        /// Atualiza um usuario
        /// </summary>
        /// <param name="usuarioServices">servico de usuario</param>
        /// <param name="usuario">dados do usuario</param>
        /// <returns>usuario atualizado</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Post([FromServices] IUsuarioServices usuarioServices, [FromBody] Usuario usuario)
        {
            try
            {
                var user = usuario;

                int.TryParse(TokenHelper.GetClaims(User.Identity, ClaimTypes.Authentication), out int id);
                usuarioServices.Save(user, id);

                return Ok(new { usuario });
            }
            catch (Exception ex)
            {
                return ValidationProblem(ex.Message);
            }
        }
        
        /// <summary>
        /// Deleta um usuario
        /// </summary>
        /// <param name="usuarioServices">Servico de usuario</param>
        /// <param name="id">codigo do usuario</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Delete([FromServices] IUsuarioServices usuarioServices, int id)
        {
            try
            {
                usuarioServices.DeleteUsuarioById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Problem(ex.Message);
            }
        }
    }
}
