using CheckInspecao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CheckInspecao.Repository.Generator;

namespace CheckInspecao.Repository.Mapping
{
    public class DocumentoInspecaoMap : IEntityTypeConfiguration<DocumentoInspecao>
    {
        public void Configure(EntityTypeBuilder<DocumentoInspecao> builder)
        {
            builder.Property(p => p.DataDocumento).ValueGeneratedOnAdd().HasValueGenerator<DataAtualGenerator>();
        }
    }
}