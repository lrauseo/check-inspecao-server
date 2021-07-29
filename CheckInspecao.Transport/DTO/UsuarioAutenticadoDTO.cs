using System;

namespace CheckInspecao.Transport.DTO
{
    public class UsuarioAutenticadoDTO
    {
        public string Token { get; set; }
        public DateTime ValidadeToken { get; set; }
        public UsuarioDTO Usuario { get; set; }
    }
}