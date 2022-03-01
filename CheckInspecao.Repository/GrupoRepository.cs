using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckInspecao.Models;
using CheckInspecao.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cadastros.Repository.GrupoRepository
{
    public interface IGrupoRepository
    {
        Task<IList<Grupo>> BuscarGrupos(string pesquisa);     
        Task<IList<Grupo>> GetGrupos();   
        Task<Grupo> CadastrarGrupo(Grupo grupo);
        Task<IList<ItemInspecao>> BuscarItensInspecao(int grupoId);

        Task<ItemInspecao> SalvarItemInspecao(ItemInspecao itemInspecao);
    }

    public class GrupoRepository : CadastroRepository, IGrupoRepository
    {
        private readonly BancoContext _context;

        public GrupoRepository(BancoContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Grupo> CadastrarGrupo(Grupo grupo)
        {
            try
            {                
                if (grupo.Id > 0 && _context.Grupos.AsNoTracking().Any(a => a.Id == grupo.Id))
                {
                    Update<Grupo>(grupo);
                }
                else
                {
                    await AddAsync<Grupo>(grupo);

                }
                await SaveChangesAsync();
                return grupo;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IList<Grupo>> BuscarGrupos(string pesquisa)
        {
            try
            {
                IQueryable<Grupo>lista = null;
                if(pesquisa == null || pesquisa == string.Empty)
                    lista = _context.Grupos;
                else
                    lista = _context.Grupos.AsNoTracking().Where(w => w.Descricao.ToLower().Contains(pesquisa.ToLower()));
                
                return await lista.ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<Grupo>> GetGrupos()
        {
            try
            {                            
                var lista = await _context.Grupos.AsNoTracking().ToListAsync();
                
                return lista;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<ItemInspecao>> BuscarItensInspecao(int grupoId)
        {
            try
            {                
                var lista = _context.ItensInspecao.AsNoTracking().Where(w => w.Grupo.Id == grupoId);                
                return await lista.ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ItemInspecao> SalvarItemInspecao(ItemInspecao itemInspecao)
        {
             try
            {    
                itemInspecao.GrupoId = itemInspecao.Grupo.Id;   
                itemInspecao.Grupo = null;         
                if (itemInspecao.Id > 0 && _context.ItensInspecao.AsNoTracking().Any(a => a.Id == itemInspecao.Id))
                {
                    Update<ItemInspecao>(itemInspecao);
                }
                else
                {
                    await AddAsync<ItemInspecao>(itemInspecao);
                }
                await SaveChangesAsync();
                return itemInspecao;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}