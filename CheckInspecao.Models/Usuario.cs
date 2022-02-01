using System.Collections.Generic;

namespace CheckInspecao.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] FotoPerfil { get; set; }
        public byte[] Assinatura { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; } = "User";

        public string Sexo { get; set; }

        public IList<UsuarioEmpresa> Empresas { get;}

    }
}