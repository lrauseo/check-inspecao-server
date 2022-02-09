using CheckInspecao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckInspecao.Repository.Mapping
{
    public class PerfilUsuarioMap : IEntityTypeConfiguration<PerfilUsuario>
    {
        public void Configure(EntityTypeBuilder<PerfilUsuario> builder)
        {
            builder.HasKey(k => k.Id);
            builder.HasOne(o => o.Usuario).WithMany(m => m.PefilUsuarios);
            builder.HasOne(o => o.Empresa);            

        }
    }
}