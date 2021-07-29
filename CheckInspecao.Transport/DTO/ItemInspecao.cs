using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckInspecao.Transport.DTO
{
    public class ItemInspecaoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Campo é Obrigatório")]
        public GrupoDTO Grupo { get; set; }
        public string Classificacao { get; set; }
        public string Descricao { get; set; }
        // public bool Sim { get; set; }
        // public bool Nao { get; set; }
        // public bool NaoSeAplica { get; set; }
        // public bool NaoObservado { get; set; }
    }
}