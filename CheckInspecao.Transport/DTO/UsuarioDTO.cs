using System.ComponentModel.DataAnnotations;

namespace CheckInspecao.Transport.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] Assinatura { get; set; }
        public byte[] FotoPerfil { get; set; }
        public EmpresaDTO Empresa { get; set; }
        public LoginDTO Login { get; set; }
    }
}