namespace CheckInspecao.Models
{
    public class Foto
    {
        public int Id { get; set; }
        public ItemDocumentoInspecao ItemInspecao { get; set; }  
        public byte[] Arquivo { get; set; }
        //http://binaryintellect.net/articles/2f55345c-1fcb-4262-89f4-c4319f95c5bd.aspx
    }
}