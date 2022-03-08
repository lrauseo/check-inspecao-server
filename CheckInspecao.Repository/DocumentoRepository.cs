using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckInspecao.Models;
using CheckInspecao.Models.EqualityCompare;
using Microsoft.EntityFrameworkCore;

namespace CheckInspecao.Repository
{
    public interface IDocumentoRepository
    {
        Task<DocumentoInspecao> AbrirInspecao(int usuarioId, int clienteId);

        Task<DocumentoInspecao> SalvarDocumento(DocumentoInspecao documento);

        Task<DocumentoInspecao> GetDocumentoById(int documentoId);

        Task<List<DocumentoInspecao>> GetDocumentos(int usuarioId, int clienteId);

        Task<QuestionarioFormulario> SalvarQuestionarioFormulario(QuestionarioFormulario questionario);

        Task<IList<QuestionarioFormulario>> BuscarTodosQuestionarios();

        Task<IList<ItemInspecao>> BuscaItensDocumentoByFormulario(int formularioId);
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

        public async Task<IList<ItemInspecao>> BuscaItensDocumentoByFormulario(int formularioId)
        {
            try
            {
                var itens = await _context
                                .ItemInspecaoQuestionarioFormularios
                                .Include(i => i.ItemInspecao)
                                    .ThenInclude(g => g.Grupo)
                                .Where(a => a.QuestionarioFormularioId == formularioId).ToListAsync();
                var itensInspecao = new List<ItemInspecao>();
                foreach (var item in itens)
                {
                    itensInspecao.Add(item.ItemInspecao);
                }

                return itensInspecao;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IList<QuestionarioFormulario>>
        BuscarTodosQuestionarios()
        {
            try
            {
                return await _context
                    .QuestionarioFormulario
                    .Include(i => i.ItensQuestionario)
                    .ThenInclude(i => i.ItemInspecao)
                    .ThenInclude(g => g.Grupo)
                    .ToListAsync();
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
                foreach (var item in documento.Itens)
                {
                    item.Item.Grupo = null;
                }
                docExistente =
                    await _context
                        .Documentos.AsNoTracking()
                        .Include(i => i.Itens).AsNoTracking()
                        .FirstOrDefaultAsync(d => d.Id == documento.Id);
                if (docExistente != null)
                    _context
                        .Entry(docExistente)
                        .CurrentValues
                        .SetValues(documento);
                //Update<DocumentoInspecao>(documento);
                else
                {

                    documento.DataDocumento = System.DateTime.Now;
                    documento.PerfilUsuarioId = documento.UsuarioInspecao.Id;
                    documento.UsuarioInspecao = null;
                    documento.Cliente = _context.Clientes.FirstOrDefault(c => c.Id == documento.Cliente.Id);
                    foreach (var itemInspecao in documento.Itens)
                    {
                        itemInspecao.Item = _context.ItensInspecao.FirstOrDefault(a => a.Id == itemInspecao.Item.Id);
                    }

                    await AddAsync<DocumentoInspecao>(documento);
                }
                await SaveChangesAsync();
                return documento;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<QuestionarioFormulario>
        SalvarQuestionarioFormulario(QuestionarioFormulario questionario)
        {
            try
            {
                foreach (var item in questionario.ItensQuestionario)
                {
                    item.ItemInspecao = null;
                    item.QuestionarioFormulario = null;
                }

                if (
                    questionario.Id > 0 &&
                    _context
                        .QuestionarioFormulario
                        .AsNoTracking()
                        .Any(a => a.Id == questionario.Id)
                )
                {
                    var itensAtuais =
                        _context
                            .ItemInspecaoQuestionarioFormularios
                            .AsNoTracking()
                            .Where(a =>
                                a.QuestionarioFormularioId == questionario.Id)
                            .ToList();

                    var itensExclusao =
                        itensAtuais
                            .Except(questionario.ItensQuestionario,
                            new ItemInspecaoQuestionarioFormularioComparer())
                            .ToList();
                    var itensInclusao =
                        questionario
                            .ItensQuestionario
                            .Except(itensAtuais,
                            new ItemInspecaoQuestionarioFormularioComparer())
                            .ToList();

                    if (itensExclusao.Count > 0)
                        DeleteRange
                        <ItemInspecaoQuestionarioFormulario>(itensExclusao
                            .ToArray());
                    foreach (var item in itensInclusao)
                    {
                        await AddAsync
                        <ItemInspecaoQuestionarioFormulario>(item);
                    }
                    Update<QuestionarioFormulario>(questionario);
                    //questionario.ItensQuestionario.Clear();
                }
                else
                {
                    await AddAsync<QuestionarioFormulario>(questionario);
                }
                var changes = await SaveChangesAsync();
                return questionario;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
