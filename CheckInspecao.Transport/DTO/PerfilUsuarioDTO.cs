namespace CheckInspecao.Transport.DTO
{
    public class PerfilUsuarioDTO
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public UsuarioDTO Usuario { get; set; }

        public int EmpresaId { get; set; }

        public EmpresaDTO Empresa { get; set; }

        public bool IsInativo { get; set; }
    }
}
