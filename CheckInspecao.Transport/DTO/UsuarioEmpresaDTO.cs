using CheckInspecao.Transport.DTO;

namespace CheckInspecao.Transport
{
    public class UsuarioEmpresaDTO
    {
        public int UsuarioId { get; set; }

        public UsuarioDTO Usuario { get; set; }

        public int EmpresaId { get; set; }

        public EmpresaDTO Empresa { get; set; }
    }
}
