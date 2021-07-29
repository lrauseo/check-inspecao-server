using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CheckInspecao.Models;
using CheckInspecao.Repository;
using CheckInspecao.Transport.DTO;
using Microsoft.Extensions.Logging;

namespace CheckInspecao.Transport
{
    public interface IDocumentoTransport
    {
        Task<DocumentoInspecaoDTO> AbrirInspecao(int usuarioId, int clienteId);
        Task<DocumentoInspecaoDTO> GetDocumentoById(int documentoId);
        Task<List<DocumentoInspecaoDTO>> GetDocumentos(int usuarioId, int clienteId);
        string GetHtmlReport(int documentoId);
        Task<DocumentoInspecaoDTO> SalvarDocumento(DocumentoInspecaoDTO documento);
    }

    public class DocumentoTransport : IDocumentoTransport
    {
        private readonly IDocumentoRepository _documentoRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentoTransport> _log;

        public DocumentoTransport(IDocumentoRepository documentoRepo, IMapper mapper, ILogger<DocumentoTransport> logger)
        {
            _documentoRepo = documentoRepo;
            _mapper = mapper;
            _log = logger;
        }
        public async Task<DocumentoInspecaoDTO> AbrirInspecao(int usuarioId, int clienteId)
        {
            try
            {
                var doc = await _documentoRepo.AbrirInspecao(usuarioId, clienteId);
                return _mapper.Map<DocumentoInspecaoDTO>(doc);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DocumentoInspecaoDTO> SalvarDocumento(DocumentoInspecaoDTO documento)
        {
            try
            {
                var documentoBanco = _mapper.Map<DocumentoInspecao>(documento);
                var doc = await _documentoRepo.SalvarDocumento(documentoBanco);
                return _mapper.Map<DocumentoInspecaoDTO>(doc);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DocumentoInspecaoDTO> GetDocumentoById(int documentoId)
        {
            try
            {
                var doc = await _documentoRepo.GetDocumentoById(documentoId);
                return _mapper.Map<DocumentoInspecaoDTO>(doc);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DocumentoInspecaoDTO>> GetDocumentos(int usuarioId, int clienteId)
        {
            try
            {
                var doc = await _documentoRepo.GetDocumentos(usuarioId, clienteId);
                return _mapper.Map<List<DocumentoInspecaoDTO>>(doc);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public string GetHtmlReport(int documentoId)
        {
            try
            {
                var employees =
                new List<EmployeeDTO>{
                    new EmployeeDTO{Name = "Mike", LastName="Turner", Age=35, Gender="Male"},
                    new EmployeeDTO{Name = "Daiane", LastName="Rauseo", Age=28, Gender="Female"},
                    new EmployeeDTO{Name = "Leandro", LastName="Rauseo", Age=35, Gender="Male"},
                    new EmployeeDTO{Name = "Tiago", LastName="Rada", Age=40, Gender="Male"},
                };
                var sb = new StringBuilder();
                sb.Append(@"
                    <html>
                    <head></head>
                    <body>
                        <div class='header' <h1>PDF Report</h1>
                        </div>
                        <table align='center'>
                            <tr>
                                <th>Name</th>
                                <th>LastName</th>
                                <th>Age</th>
                                <th>Gender</th>
                            </tr>                 
                ");
                foreach (var item in employees)
                {
                    sb.AppendFormat(@"
                    <tr>
                        <td>{0}</td>
                        <td>{1}</td>
                        <td>{2}</td>
                        <td>{3}</td>
                    </tr>
                    ", item.Name
                         , item.LastName
                         , item.Age
                         , item.Gender
                    );
                }
                sb.Append(@"</table>");
                sb.Append(@"</body>");
                sb.Append(@"</html>");
                
                return sb.ToString();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}