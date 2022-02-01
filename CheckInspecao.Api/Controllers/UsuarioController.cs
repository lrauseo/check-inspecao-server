using System.Security.Claims;
using System.Threading.Tasks;
using CheckInspecao.Transport;
using CheckInspecao.Transport.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CheckInspecao.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "User,Admin")]
    public class UsuarioController :ControllerBase
    {
        private readonly ILogger<IUsuarioTransport> _log;
        private readonly IUsuarioTransport _usuarioTransport;
        

        public UsuarioController(ILogger<IUsuarioTransport> logger, IUsuarioTransport usuarioTransport)
        {
            _log = logger;
            _usuarioTransport = usuarioTransport;            
        }
        [HttpPost("SalvarUsuario")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SalvarUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                var usuario = await _usuarioTransport.SalvarUsuario(usuarioDTO);
                return Ok(usuario);
            }
            catch (System.Exception ex)
            {                
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AutenticarUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> AutenticarUsuario(string login, string senha)
        {
            try
            {                
                var usuarioAuth = await _usuarioTransport.AutenticaUsuario(login, senha);
                return Ok(usuarioAuth);
            }
            catch (System.Exception ex)
            {                
                return BadRequest(ex);
            }
        }
    }
}