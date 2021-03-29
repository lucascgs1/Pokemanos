using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pokemanos.Api.Model;
using Pokemanos.Model;
using Pokemanos.Services;
using Pokemanos.Services.Interfaces;
using Pokemones.API.Helper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Pokemanos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly ILogger<UsuarioController> _logger;

        public AccountController(
            ILogger<UsuarioController> logger,
            IOptions<AppSettings> appSettings
            )
        {
            this._logger = logger;
            this.appSettings = appSettings;
        }


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Autenticar([FromServices] IUsuarioServices usuarioServices, [FromBody] LoginViewModel model)
        {
            try
            {
                var usuario = await usuarioServices.AuthenticateAsync(model.Email, model.Senha);

                if (usuario == null)
                    throw new ValidationException("Usuário ou senha inválidos!");

                if (usuario.Senha != model.Senha.Md5Hash())
                    throw new ValidationException("Senha invalida!");

                var token = TokenHelper.GenerateToken(usuario, appSettings.Value.Secret);

                return Ok(new { usuario, token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("cadastro")]
        [AllowAnonymous]
        public IActionResult Cadastro([FromServices] IUsuarioServices usuarioServices, [FromBody] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = usuarioServices.Save(usuario);
                    var token = TokenHelper.GenerateToken(newUser, appSettings.Value.Secret);

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
    }
}
