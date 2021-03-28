using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pokemanos.Api.Model;
using Pokemanos.Model;
using Pokemanos.Services.Interfaces;
using Pokemones.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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



        [HttpPost]
        [Route("cadastro")]
        [AllowAnonymous]
        public IActionResult Register([FromServices] IUsuarioServices usuarioServices, [FromBody] Usuario usuario)
        {
            try
            {
                var teste = this.appSettings;

                var teste2 = Environment.GetEnvironmentVariable("AppSettings");
                if (ModelState.IsValid)
                {
                    var newUser = usuarioServices.Save(usuario);
                    var token = TokenHelper.GenerateToken(newUser, this.appSettings.Value.Secret);

                    return Ok(new { usuario = newUser, token });
                }
                else
                {
                    return ValidationProblem();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
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
    }
}
