using System.Collections.Generic;

namespace CheckInspecao.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string UsuarioLogin { get; set; }
        public string Senha { get; set; }

        public IList<Usuario> Usuarios { get; set; }
        public IList<Cliente> Clientes { get; set; }
    }
}