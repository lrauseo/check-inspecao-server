

namespace CheckInspecao.Transport.DTO
{
    public class ItemInspecaoQuestionarioFormularioDTO
    {
        public int ItemInspecaoId { get; set; }
        public ItemInspecaoDTO ItemInspecao { get; set; }
        public int QuestionarioFormularioId { get; set; }
        public QuestionarioFormularioDTO QuestionarioFormulario { get; set; }
    }
}
