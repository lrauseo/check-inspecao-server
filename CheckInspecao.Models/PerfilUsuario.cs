namespace CheckInspecao.Models
{
    public class PerfilUsuario
    {
        public int Id { get; set; } 
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        //public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public bool IsInativo { get; set; }
    }
}