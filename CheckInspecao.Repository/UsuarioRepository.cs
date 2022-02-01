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
                                        .Include(i => i.Empresas)
                                        .FirstOrDefaultAsync(a => a.Email == login
                                            && a.Senha == senha);
                return usuarioAuth;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}