using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckInspecao.Transport.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] FotoPerfil { get; set; }
        public byte[] Assinatura { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }

        public string Sexo { get; set; }

        public IList<UsuarioEmpresaDTO> Empresas { get; set; }
    }
}