using System;
using System.Threading.Tasks;
using AutoMapper;
using CheckInspecao.Models;
using CheckInspecao.Repository;
using CheckInspecao.Transport.DTO;

namespace CheckInspecao.Transport
{
    public interface IUsuarioTransport
    {
        Task<UsuarioAutenticadoDTO> AutenticaUsuario(string login, string senha);
        Task<UsuarioDTO> SalvarUsuario(UsuarioDTO usuario);
    }

    public class UsuarioTransport : IUsuarioTransport
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioTransport(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task<UsuarioAutenticadoDTO> AutenticaUsuario(string login, string senha)
        {
            try
            {
                var usuario = await _usuarioRepository.AutenticaUsuario(login, senha);
                if (usuario == null)
                    throw new System.Exception("Usuario/Senha Inválidos");
                var autenticado = new UsuarioAutenticadoDTO()
                {
                    Usuario = _mapper.Map<UsuarioDTO>(usuario),
                    Token = "aqui vai um token",
                    ValidadeToken = DateTime.Now.AddDays(1)
                };
                return autenticado;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<UsuarioDTO> SalvarUsuario(UsuarioDTO usuario)
        {
            try
            {
                if(usuario.Login == null)
                    throw new Exception("Login não cadastrado");
                var usuarioDB = _mapper.Map<Usuario>(usuario);
                var usuarioSalvo = await _usuarioRepository.SalvarUsuario(usuarioDB);
                return _mapper.Map<UsuarioDTO>(usuarioSalvo);

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}
