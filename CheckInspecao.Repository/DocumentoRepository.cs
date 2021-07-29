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
        Task<List<DocumentoInspecao>> GetDocumentos(int usuarioId, int clienteId);
    }

    public class DocumentoRepository : CadastroRepository, IDocumentoRepository
    {
        private readonly BancoContext _context;

        public DocumentoRepository(BancoContext context) : base(context)
        {
            _context = context;
        }
        public async Task<DocumentoInspecao> AbrirInspecao(int usuarioId, int clienteId)
        {
            try
            {
                var doc = new DocumentoInspecao()
                {
                    UsuarioInspecao = await _context.Usuarios.FirstOrDefaultAsync(a => a.Id == usuarioId),
                    Cliente = await _context.Clientes.FirstOrDefaultAsync(a => a.Id == clienteId)
                };
                await AddAsync<DocumentoInspecao>(doc);
                await SaveChangesAsync();
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
                var doc = await _context.Documentos
                                  .Include(i => i.Itens)
                                    .ThenInclude(ti => ti.Item)
                                    .ThenInclude(tii => tii.Grupo)
                                  .Include(i => i.Itens).ThenInclude(d => d.Documento)
                                  .Include(i => i.Itens).ThenInclude(d => d.Fotos).ThenInclude(e => e.ItemInspecao)
                                  .FirstOrDefaultAsync(f => f.Id == documentoId);
                return doc;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DocumentoInspecao>> GetDocumentos(int usuarioId, int clienteId)
        {
            try
            {
                var doc = await _context.Documentos
                                  .Include(i => i.Cliente)
                                  .Include(i => i.UsuarioInspecao)
                                  .Where(f => f.UsuarioInspecao.Id == usuarioId
                                                        && f.Cliente.Id == clienteId).ToListAsync();
                return doc;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DocumentoInspecao> SalvarDocumento(DocumentoInspecao documento)
        {
            try
            {
                var docExistente = await _context.Documentos
                                            .Include(i => i.Itens).ThenInclude(i1 => i1.Fotos)
                                            .FirstOrDefaultAsync(d => d.Id == documento.Id);
                _context.Entry(docExistente).CurrentValues.SetValues(documento);                
                // await SaveChangesAsync();
                // var itensExcluidos = documento.Itens.Except(docExistente.Itens).ToList();
                foreach (var item in documento.Itens)
                {

                    var itemExistente =  docExistente.Itens.FirstOrDefault(a => a.Id == item.Id);
                    if (itemExistente != null)
                    {
                        _context.Entry(itemExistente).CurrentValues.SetValues(item);
                            // _context.Entry(foto.ItemInspecao).State = EntityState.Detached;
                        foreach (var foto in item.Fotos)
                        {
                            //foto.ItemInspecao = null;  
                            // _context.Attach(foto.ItemInspecao);                            
                            var fotoExiste =  itemExistente.Fotos.FirstOrDefault(a => a.Id == foto.Id);
                            if(fotoExiste != null)
                                _context.Entry(fotoExiste).CurrentValues.SetValues(foto);
                            else
                            {                                
                                itemExistente.Fotos.Add(foto);
                            }
                        }
                    }
                    else
                    {
                        item.Documento = docExistente;
                        item.Item = await _context.ItensInspecao
                                                  .FirstOrDefaultAsync(a => a.Id == item.Item.Id) ?? item.Item;
                        foreach (var foto in item.Fotos)
                        {
                            foto.ItemInspecao = item;
                        }
                        Update<ItemDocumentoInspecao>(item);
                    }
                    // if (itensExcluidos.Count() > 0)
                    //     _context.ItemDocumentoInspecao.RemoveRange(itensExcluidos);
                    // Update<ItemDocumentoInspecao>(item);
                    await SaveChangesAsync();

                }
                return documento;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}