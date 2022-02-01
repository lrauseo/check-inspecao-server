using System;
using System.Threading.Tasks;
using AutoMapper;
using CheckInspecao.Models;
using CheckInspecao.Repository;
using CheckInspecao.Transport.Criptogratia;
using CheckInspecao.Transport.DTO;
using Microsoft.Extensions.Options;

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

        public ITokenTransport _tokenTransport { get; }

        private readonly IOptions<CriptografiaConfigurationDTO> _criptografiaConfig;
        private readonly CriptografiaHelper _criptografiaHelper;

        public UsuarioTransport(IUsuarioRepository usuarioRepository, IMapper mapper, ITokenTransport tokenTransport, IOptions<CriptografiaConfigurationDTO> criptografiaConfig)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _tokenTransport = tokenTransport;
            _criptografiaConfig = criptografiaConfig;
            _criptografiaHelper = new CriptografiaHelper(_criptografiaConfig);
            
        }
        public async Task<UsuarioAutenticadoDTO> AutenticaUsuario(string login, string senha)
        {
            try
            {
                var _criptografiaHelper = new CriptografiaHelper(_criptografiaConfig);
                 var senhaCriptoteste = _criptografiaHelper.AesEncrypt("123");
                var senhaCripto = senha;
                var senhaText = _criptografiaHelper.AesDecrypt(senhaCripto);
                var senhaUsuario = _criptografiaHelper.HMacSha256Encrypt(senhaText);
                var usuario = await _usuarioRepository.AutenticaUsuario(login, senhaUsuario);

                if (usuario == null)
                    throw new System.Exception("Usuario/Senha Inválidos");

                var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
                
                var tokenDTO =  _tokenTransport.GetTokenDTO(usuarioDto);
                var autenticado = new UsuarioAutenticadoDTO()
                {
                    Token = tokenDTO.Token,
                    ValidadeToken = tokenDTO.Expiration
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
                var usuarioDB = _mapper.Map<Usuario>(usuario);
                var _criptografiaHelper = new CriptografiaHelper(_criptografiaConfig);
                var senhaText = _criptografiaHelper.AesDecrypt(usuario.Senha);
                usuarioDB.Senha = _criptografiaHelper.HMacSha256Encrypt(senhaText);

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
