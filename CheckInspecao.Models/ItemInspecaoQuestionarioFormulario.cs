namespace CheckInspecao.Models
{
    public class ItemInspecaoQuestionarioFormulario
    {
        public int ItemInspecaoId { get; set; }
        public ItemInspecao ItemInspecao { get; set; }
        public int QuestionarioFormularioId { get; set; }
        public QuestionarioFormulario QuestionarioFormulario { get; set; }

    }
}