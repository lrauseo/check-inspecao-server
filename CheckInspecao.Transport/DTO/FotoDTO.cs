using System.ComponentModel.DataAnnotations;

namespace CheckInspecao.Transport.DTO
{
    public class FotoDTO
    {
        public int Id { get; set; }        
        public ItemDocumentoInspecaoDTO ItemInspecao { get; set; }
        public byte[] Arquivo { get; set; }
    }
}