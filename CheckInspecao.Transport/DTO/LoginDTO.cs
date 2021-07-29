using System.Collections.Generic;

namespace CheckInspecao.Transport.DTO
{
    public class LoginDTO
    {
        public int Id { get; set; }
        public string UsuarioLogin { get; set; }
        public string Senha { get; set; }

        public IList<UsuarioDTO> Usuarios { get; set; }
        public IList<ClienteDTO> Clientes { get; set; }
    }
}