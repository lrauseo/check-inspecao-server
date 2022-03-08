using CheckInspecao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckInspecao.Repository.Mapping
{
    public class ItemInspecaoQuestionarioFormularioMap : IEntityTypeConfiguration<ItemInspecaoQuestionarioFormulario>
    {
        public void Configure(EntityTypeBuilder<ItemInspecaoQuestionarioFormulario> builder)
        {
            builder.HasKey(k => new { k.ItemInspecaoId, k.QuestionarioFormularioId});
        }
    }
}