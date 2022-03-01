using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckInspecao.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckInspecao.Repository
{
    public interface IDocumentoRepository
    {
        Task<DocumentoInspecao> AbrirInspecao(int usuarioId, int clienteId);

        Task<DocumentoInspecao> SalvarDocumento(DocumentoInspecao documento);

        Task<DocumentoInspecao> GetDocumentoById(int documentoId);

        Task<List<DocumentoInspecao>>
        GetDocumentos(
            int usuarioId, int clienteId
        );
    }

    public class DocumentoRepository : CadastroRepository, IDocumentoRepository
    {
        private readonly BancoContext _context;

        public DocumentoRepository(BancoContext context) :
            base(context)
        {
            _context = context;
        }

        public async Task<DocumentoInspecao>
        AbrirInspecao(int perfilId, int clienteId)
        {
            try
            {                
                var doc =
                    new DocumentoInspecao()
                    {
                        UsuarioInspecao =
                            await _context
                                .PerfilUsuarios
                                .Include(u => u.Usuario)
                                .Include(e => e.Empresa)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(a => a.Id == perfilId),
                        Cliente =
                            await _context
                                .Clientes
                                .AsNoTracking()
                                .FirstOrDefaultAsync(a => a.Id == clienteId)
                    };

                doc.PerfilUsuarioId = perfilId;

                // await AddAsync<DocumentoInspecao>(doc);
                // await SaveChangesAsync();
                return doc;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DocumentoInspecao> GetDocumentoById(int documentoId)
        {
            try
            {
                var doc =
                    await _context
                        .Documentos
                        .Include(i => i.Itens)
                        .ThenInclude(ti => ti.Item)
                        .ThenInclude(tii => tii.Grupo)
                        .Include(i => i.Itens)
                        .ThenInclude(d => d.Documento)
                        .Include(i => i.Itens)
                        .ThenInclude(d => d.Fotos)
                        .ThenInclude(e => e.ItemInspecao)
                        .FirstOrDefaultAsync(f => f.Id == documentoId);
                return doc;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DocumentoInspecao>>
        GetDocumentos(int usuarioId, int clienteId)
        {
            try
            {
                var doc =
                    await _context
                        .Documentos
                        .Include(i => i.Cliente)
                        .Include(i => i.UsuarioInspecao)
                        .Where(f =>
                            f.UsuarioInspecao.Id == usuarioId &&
                            f.Cliente.Id == clienteId)
                        .ToListAsync();
                return doc;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DocumentoInspecao>
        SalvarDocumento(DocumentoInspecao documento)
        {
            DocumentoInspecao docExistente;
            try
            {
                docExistente =
                    await _context
                        .Documentos
                        .Include(i => i.Itens)
                        .ThenInclude(i1 => i1.Fotos)
                        .FirstOrDefaultAsync(d => d.Id == documento.Id);
                if(docExistente != null)
                    _context.Entry(docExistente).CurrentValues.SetValues(documento);
                else{ 
                    foreach (var item in documento.Itens)
                    {
                        item.Item.Grupo = null;
                    }
                    documento.DataDocumento = System.DateTime.Now;
                    documento.PerfilUsuarioId = documento.UsuarioInspecao.Id;
                    documento.UsuarioInspecao = null;
                    Update(documento);
                }
                await SaveChangesAsync();
                return documento;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
