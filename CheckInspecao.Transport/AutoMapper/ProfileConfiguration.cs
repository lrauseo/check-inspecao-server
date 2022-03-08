using AutoMapper;
using CheckInspecao.Models;
using CheckInspecao.Transport;
using CheckInspecao.Transport.DTO;

namespace Cadastros.Transport.AutoMapper
{
    public class ProfileConfiguration : Profile
    {
        public ProfileConfiguration()
        {
            CreateMap<Usuario,UsuarioDTO>()
            .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => "**secret**"));
            CreateMap<UsuarioDTO, Usuario>();            
            CreateMap<UsuarioEmpresa,UsuarioEmpresaDTO>().ReverseMap();
            CreateMap<Grupo,GrupoDTO>().ReverseMap();            
            CreateMap<DocumentoInspecao,DocumentoInspecaoDTO>().ReverseMap();
            CreateMap<ItemInspecao,ItemInspecaoDTO>().ReverseMap();
            CreateMap<ItemDocumentoInspecao,ItemDocumentoInspecaoDTO>()
            .ForMember(dest => dest.DocumentoPai, 
                                opt => {
                                    opt.MapFrom(src => src.Documento);
                                }).ReverseMap();            
            
            CreateMap<Cliente,ClienteDTO>().ReverseMap();
            CreateMap<Empresa,EmpresaDTO>().ReverseMap();
            CreateMap<PerfilUsuario,PerfilUsuarioDTO>().ReverseMap();
            CreateMap<Foto,FotoDTO>().ReverseMap();
            CreateMap<QuestionarioFormulario, QuestionarioFormularioDTO>().ReverseMap();
            CreateMap<ItemInspecaoQuestionarioFormulario, ItemInspecaoQuestionarioFormularioDTO>().ReverseMap();
        }
    }
}