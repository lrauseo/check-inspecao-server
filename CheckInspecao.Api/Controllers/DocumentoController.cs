using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CheckInspecao.Models;
using CheckInspecao.Transport;
using CheckInspecao.Transport.DTO;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CheckInspecao.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumentoTransport _documentoTransport;

        private readonly IConverter _converter;

        private readonly ILogger<DocumentoController> _log;

        public DocumentoController(
            IDocumentoTransport documentoTransport,
            IConverter converter,
            ILogger<DocumentoController> logger
        )
        {
            _documentoTransport = documentoTransport;
            _converter = converter;
            _log = logger;
        }

        [HttpPost("NovoDocumento")]
        public async Task<IActionResult>
        NovoDocumento(int clienteId, int perfilUsuarioId)
        {
            try
            {
                // var identity = HttpContext.User.Identity as ClaimsIdentity;
                // var claims = identity.Claims as List<Claim>;
                // var usuarioId =
                //     int.Parse(claims.FirstOrDefault(f => f.Type == "id").Value);
                var doc =
                    await _documentoTransport
                        .AbrirInspecao(perfilUsuarioId, clienteId);
                return Ok(doc);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SalvarDocumeto")]
        public async Task<IActionResult>
        SalvarDocumento(DocumentoInspecaoDTO documento)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var claims = identity.Claims as List<Claim>;
                var usuarioId =
                    int.Parse(claims.FirstOrDefault(f => f.Type == "id").Value);

                var doc = await _documentoTransport.SalvarDocumento(documento);
                return Ok(doc);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetDocumentoById")]
        public async Task<IActionResult> GetDocumentoById(int documentoId)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var claims = identity.Claims as List<Claim>;
                var usuarioId =
                    int.Parse(claims.FirstOrDefault(f => f.Type == "id").Value);
                var nomeUsuario =
                    claims.FirstOrDefault(f => f.Type == "name").Value;
                var doc =
                    await _documentoTransport.GetDocumentoById(documentoId);
                return Ok(doc);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetDocumentos")]
        public async Task<IActionResult> GetDocumentos(int clienteId)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var claims = identity.Claims as List<Claim>;
                var usuarioId =
                    int.Parse(claims.FirstOrDefault(f => f.Type == "id").Value);
                var nomeUsuario =
                    claims.FirstOrDefault(f => f.Type == "name").Value;
                var doc =
                    await _documentoTransport
                        .GetDocumentos(usuarioId, clienteId);
                return Ok(doc);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("QuestionarioFormulario")]
        public async Task<IActionResult>
        SalvarQuestionarioFormulario(
            QuestionarioFormularioDTO questionarioFormularioDTO
        )
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var claims = identity.Claims as List<Claim>;
                var usuarioId =
                    int.Parse(claims.FirstOrDefault(f => f.Type == "id").Value);

                var doc =
                    await _documentoTransport
                        .SalvarQuestionarioFormulario(questionarioFormularioDTO);
                return Ok(doc);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("QuestionarioFormulario")]
        public async Task<IActionResult> GetQuestionarios()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var claims = identity.Claims as List<Claim>;
                var usuarioId =
                    int.Parse(claims.FirstOrDefault(f => f.Type == "id").Value);

                var doc = await _documentoTransport.GetQuestionarios();
                return Ok(doc);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ItemInspecaoByFormulario")]
        public async Task<IActionResult> BuscaItemInspecaoByFormulario(int formularioId)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var claims = identity.Claims as List<Claim>;
                var usuarioId =
                    int.Parse(claims.FirstOrDefault(f => f.Type == "id").Value);

                var doc = await _documentoTransport.BuscaItensDocumentoByFormulario(formularioId);
                return Ok(doc);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CreatePDF")]
        public IActionResult CreatePDF()
        {
            var globalSettings =
                new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report"
                    //para criar um arquivo direto em um diretorio
                    //Out = (@"PDF" + Path.DirectorySeparatorChar + "Employee_Report.pdf")
                };
            var html = _documentoTransport.GetHtmlReport(1);
            _log.LogInformation(html);
            _log.LogError(html);
            var objectSettings =
                new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = html,
                    //opcao para gerar pdf da pagina inteira
                    //Page = "https://code-maze.com";
                    WebSettings =
                        {
                            UserStyleSheet =
                                Path
                                    .Combine(Directory.GetCurrentDirectory(),
                                    "Assets",
                                    "styles.css")
                        }
                    // HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page[page]of[toPage]", Line = true },
                    // FooterSettings = { FontName = "Arial", FontSize = 9, Center = "Report Footer", Line = true }
                };
            var pdf =
                new HtmlToPdfDocument
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

            // return Ok("PDF Criado com sucesso!"); quando for gerar arquivo num diretorio
            /*Gerar arquivo no brownser*/
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "arquivo.pdf");
        }
    }
}
