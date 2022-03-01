using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cadastros.Repository.GrupoRepository;
using CheckInspecao.Models;
using CheckInspecao.Transport.DTO;
using CheckInspecao.Transport.Exceptions;

namespace CheckInspecao.Transport.GrupoTransport
{
    public interface IGrupoTransport
    {
        Task<IList<GrupoDTO>> BuscarGrupos(string pesquisa);
        Task<IList<GrupoDTO>> GetGrupos();        
        Task<GrupoDTO> CadastrarGrupo(GrupoDTO grupoDTO);
        Task<IList<ItemInspecaoDTO>> BuscarItensInspecao(int grupoId);

        Task<ItemInspecaoDTO> SalvarItemInspecao(ItemInspecaoDTO itemInspecaoDTO);
    }

    public class GrupoTransport : IGrupoTransport
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly IMapper _mapper;

        public GrupoTransport(IGrupoRepository grupoRepository, IMapper mapper)
        {
            _grupoRepository = grupoRepository;
            _mapper = mapper;
        }

        public async Task<GrupoDTO> CadastrarGrupo(GrupoDTO grupoDTO)
        {
            try
            {
                var gp = _mapper.Map<Grupo>(grupoDTO);
                gp = await _grupoRepository.CadastrarGrupo(gp);
                return _mapper.Map<GrupoDTO>(gp);
            }
            catch (System.Exception ex)
            {
                throw new CadastrosException(ex.Message);
            }
        }
        public async Task<IList<GrupoDTO>> BuscarGrupos(string pesquisa)
        {
            try
            {
                var grupos = await _grupoRepository.BuscarGrupos(pesquisa);
                return _mapper.Map<IList<GrupoDTO>>(grupos);
            }
            catch (System.Exception ex)
            {
                throw new CadastrosException(ex.Message);
            }
        }
    
        public async Task<IList<GrupoDTO>> GetGrupos()
        {
           try
            {
                var grupos = await _grupoRepository.GetGrupos();
                return _mapper.Map<IList<GrupoDTO>>(grupos);
            }
            catch (System.Exception ex)
            {
                throw new CadastrosException(ex.Message);
            }
        } 
        public async Task<IList<ItemInspecaoDTO>> BuscarItensInspecao(int grupoId)
        {
            try
            {                
                var lista = await _grupoRepository.BuscarItensInspecao(grupoId);                
                return  _mapper.Map<List<ItemInspecaoDTO>>(lista);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ItemInspecaoDTO> SalvarItemInspecao(ItemInspecaoDTO itemInspecaoDTO)
        {
             try
            {
                var item = _mapper.Map<ItemInspecao>(itemInspecaoDTO);
                item = await _grupoRepository.SalvarItemInspecao(item);
                return _mapper.Map<ItemInspecaoDTO>(item);
            }
            catch (System.Exception ex)
            {
                throw new CadastrosException(ex.Message);
            }
        }
    }
}

