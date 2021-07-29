using System.Collections.Generic;

namespace CheckInspecao.Transport.DTO
{
    public class EmpresaDTO
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public IList<UsuarioDTO> Usuarios { get; set; }
    }

}