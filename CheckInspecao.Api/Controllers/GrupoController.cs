using System.Threading.Tasks;
using CheckInspecao.Transport.Exceptions;
using CheckInspecao.Transport.GrupoTransport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CheckInspecao.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GrupoController : ControllerBase
    {
        private readonly ILogger<GrupoTransport> _logger;
        private readonly IGrupoTransport _grupoTransport;

        public GrupoController(ILogger<GrupoTransport> logger, IGrupoTransport grupo)
        {
            _logger = logger;
            _grupoTransport = grupo;
        }

        [HttpGet]
        public async Task<IActionResult> GetGrupos()
        {
            try
            {
                var grupos = await _grupoTransport.GetGrupos();
                return Ok(grupos);
            }
            catch (System.Exception e)
            {
                if (e is CadastrosException)
                {
                    var ex = e as CadastrosException;
                    _logger.LogError($"Erro ao buscar grupo :{ex.ToMessage()}");
                    return BadRequest($"Erro ao buscar grupo : {ex.ToMessage()}");
                }
                else
                {
                    var msg = e.InnerException == null ? e.Message : e.InnerException.Message;
                    _logger.LogError($"Erro :{msg}");
                    return BadRequest($"Erro : {msg}");
                }
            }

        }
        [HttpGet("BuscaItensInspecao")]
        public async Task<IActionResult> BuscarItensInspecao(int grupoId)
        {
            try
            {
                var lista = await _grupoTransport.BuscarItensInspecao(grupoId);
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}