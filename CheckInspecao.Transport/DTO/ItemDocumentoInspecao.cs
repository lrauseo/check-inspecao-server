using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckInspecao.Transport.DTO
{
    public class ItemDocumentoInspecaoDTO
    {
        public int Id { get; set; }        
        public DocumentoInspecaoDTO DocumentoPai { get; set; }        
        public ItemInspecaoDTO Item { get; set; }
        public bool Sim { get; set; }
        public bool Nao { get; set; }
        public bool NaoSeAplica { get; set; }
        public string Observacao { get; set; }
        public bool NaoObservado { get; set; }
        public IList<FotoDTO> Fotos { get; set; }
    }
}