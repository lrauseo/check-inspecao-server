using System.Collections.Generic;

namespace CheckInspecao.Models
{
    public class QuestionarioFormulario
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public IList<ItemInspecaoQuestionarioFormulario> ItensQuestionario { get; set; }
        public bool IsInativo { get; set; }
    }
}