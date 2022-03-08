using System.Collections.Generic;
using CheckInspecao.Transport.DTO;

namespace CheckInspecao.Transport.DTO
{
    public class QuestionarioFormularioDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public IList<ItemInspecaoQuestionarioFormularioDTO> ItensQuestionario { get; set; }
        public bool IsInativo { get; set; }
    }
}