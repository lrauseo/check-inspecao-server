using System.Threading.Tasks;
using CheckInspecao.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckInspecao.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> AutenticaUsuario(string login, string senha);
        Task<Usuario> SalvarUsuario(Usuario usuario);
    }

    public class UsuarioRepository : CadastroRepository, IUsuarioRepository
    {
        private readonly BancoContext _context;

        public UsuarioRepository(BancoContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Usuario> SalvarUsuario(Usuario usuario)
        {
            try
            {
                var empresa = await _context.Empresas.FirstOrDefaultAsync(e => e.Cnpj == usuario.Empresa.Cnpj);
                if (empresa == null)
                    throw new System.Exception("Empresa não cadastrada, favor entrar em contato com o Administrador");
                else
                    usuario.Empresa = empresa;
                Update<Usuario>(usuario);
                await SaveChangesAsync();
                return usuario;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Usuario> AutenticaUsuario(string login, string senha)
        {
            try
            {
                var usuarioAuth = await _context.Usuarios
                                        .Include(i => i.Login)
                                        .Include(i => i.Empresa)
                                        .FirstOrDefaultAsync(a => a.Login.UsuarioLogin.ToLower() == login
                                            && a.Login.Senha == senha);
                return usuarioAuth;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}