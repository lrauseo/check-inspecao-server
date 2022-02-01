using CheckInspecao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInspecao.Repository.Mapping
{
    public class UsuarioEmpresaMap : IEntityTypeConfiguration<UsuarioEmpresa>
    {
        public void Configure(EntityTypeBuilder<UsuarioEmpresa> builder)
        {
            builder.HasKey(k => new { k.EmpresaId, k.UsuarioId });
            builder.HasOne(o => o.Usuario).WithMany(m => m.Empresas).HasForeignKey(f => f.UsuarioId);
            builder.HasOne(o => o.Empresa).WithMany(m => m.Usuarios).HasForeignKey(f => f.EmpresaId);
        }
    }
}
