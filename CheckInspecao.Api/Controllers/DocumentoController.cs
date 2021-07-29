using System.IO;
using System.Threading.Tasks;
using CheckInspecao.Transport;
using CheckInspecao.Transport.DTO;
using DinkToPdf;
using DinkToPdf.Contracts;
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

        public DocumentoController(IDocumentoTransport documentoTransport, IConverter converter, ILogger<DocumentoController> logger)
        {
            _documentoTransport = documentoTransport;
            _converter = converter;
            _log = logger;
        }
        [HttpPost("NovoDocumento")]
        public async Task<IActionResult> NovoDocumento(int usuarioId, int clienteId)
        {
            try
            {
                var doc = await _documentoTransport.AbrirInspecao(usuarioId,clienteId);
                return Ok(doc);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("SalvarDocumeto")]
        public async Task<IActionResult> SalvarDocumento(DocumentoInspecaoDTO documento)
        {
            try
            {
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
                var doc = await _documentoTransport.GetDocumentoById(documentoId);
                return Ok(doc);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }[HttpGet("GetDocumentos")]
        public async Task<IActionResult> GetDocumentos(int usuarioId, int clienteId)
        {
            try
            {
                var doc = await _documentoTransport.GetDocumentos(usuarioId, clienteId);
                return Ok(doc);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("CreatePDF")]
        public IActionResult CreatePDF()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                //para criar um arquivo direto em um diretorio
                //Out = (@"PDF" + Path.DirectorySeparatorChar + "Employee_Report.pdf")
            };
            var html = _documentoTransport.GetHtmlReport(1);
            _log.LogInformation(html);
            _log.LogError(html);
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = html,
                //opcao para gerar pdf da pagina inteira
                //Page = "https://code-maze.com";
                WebSettings = { UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "styles.css") },
                // HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page[page]of[toPage]", Line = true },
                // FooterSettings = { FontName = "Arial", FontSize = 9, Center = "Report Footer", Line = true }
            };
            var pdf = new HtmlToPdfDocument
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