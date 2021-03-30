using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pokemanos.Api.Model;
using Pokemanos.Model;
using Pokemanos.Model.DTO;
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
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<UsuarioController> _logger;

        public AccountController(
            ILogger<UsuarioController> logger,
            IOptions<AppSettings> appSettings
            )
        {
            _logger = logger;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Login na aplicacao
        /// </summary>
        /// <param name="usuarioServices">servico de usuario</param>
        /// <param name="login">dados de login</param>
        /// <returns>usuario e token de autenticacao</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Autenticar([FromServices] IUsuarioServices usuarioServices, [FromBody] LoginViewModel login)
        {
            try
            {
                var usuario = await usuarioServices.AuthenticateAsync(login.Email, login.Senha);

                if (usuario == null)
                    throw new ValidationException("Usuário ou senha inválidos!");

                if (usuario.Senha != login.Senha.Md5Hash())
                    throw new ValidationException("Senha invalida!");

                var token = TokenHelper.GenerateToken(usuario, _appSettings.Value.Secret);

                return Ok(new { usuario, token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cadastra um novo usuario
        /// </summary>
        /// <param name="usuarioServices">serivico de usuario</param>
        /// <param name="usuario">dados do usuario</param>
        /// <returns>dados do usuario e token de autenticacao</returns>

        [HttpPost("cadastro")]
        [AllowAnonymous]
        public IActionResult Cadastro([FromServices] IUsuarioServices usuarioServices, [FromBody] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = usuarioServices.Save(usuario);
                    var token = TokenHelper.GenerateToken(newUser, _appSettings.Value.Secret);

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

        /// <summary>
        /// Gera um codigo de seguranca para alteracao de senha
        /// </summary>
        /// <param name="usuarioServices">servico de usuario</param>
        /// <param name="email">email do usuario</param>
        /// <returns></returns>
        [HttpPost("gerarCodigoSeguranca/{email}")]
        [AllowAnonymous]
        public IActionResult PostGerarCodigoSeguranca([FromServices] IUsuarioServices usuarioServices, string email)
        {
            try
            {
                usuarioServices.GerarCodigoSeguranca(email);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Altera a senha do usuario
        /// </summary>
        /// <param name="usuarioServices">servico de usuario</param>
        /// <param name="trocarSenha">dto com email, senha e codigo de seguranca</param>
        /// <returns></returns>
        [HttpPost("recuperarSenha")]
        [AllowAnonymous]
        public IActionResult PostRecuperarSenha([FromServices] IUsuarioServices usuarioServices, [FromBody] TrocarSenhaDTO trocarSenha)
        {
            try
            {
                usuarioServices.TrocarSenhaComCodigo(trocarSenha);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
