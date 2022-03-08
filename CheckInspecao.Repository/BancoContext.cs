using System;
using CheckInspecao.Models;
using CheckInspecao.Repository.Mapping;
using Microsoft.EntityFrameworkCore;


namespace CheckInspecao.Repository
{
    public class BancoContext : DbContext
    {
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<ItemInspecao> ItensInspecao { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<DocumentoInspecao> Documentos { get; set; }
        public DbSet<ItemDocumentoInspecao> ItemDocumentoInspecao { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<PerfilUsuario> PerfilUsuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<UsuarioEmpresa> UsuariosEmpresas { get; set; }        

        public DbSet<QuestionarioFormulario> QuestionarioFormulario { get; set; }
        public DbSet<ItemInspecaoQuestionarioFormulario> ItemInspecaoQuestionarioFormularios { get; set; }
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DocumentoInspecaoMap());
            modelBuilder.ApplyConfiguration(new UsuarioEmpresaMap());            
            modelBuilder.ApplyConfiguration(new ItemInspecaoQuestionarioFormularioMap());  

        }
    }

}
