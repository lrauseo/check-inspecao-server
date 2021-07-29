namespace CheckInspecao.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] FotoPerfil { get; set; }
        public byte[] Assinatura { get; set; }
        public Login Login { get; set; }    
        public Empresa Empresa { get; set; }    
    }
}