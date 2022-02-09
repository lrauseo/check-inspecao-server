using CheckInspecao.Models;
using CheckInspecao.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace CheckInspecao
{
    public class DbInitializer
    {
        public static void Initialize(BancoContext context)
        {
            // context.Database.EnsureCreated();
            context.Database.Migrate();

            SeedGrupos(context);
            SeedItemInspecao(context);
            SeedUsuario(context);
            SeedCliente(context);
        }

        private static void SeedItemInspecao(BancoContext context)
        {
            ItemInspecao[] ItemInspecoes = JsonConvert
                            .DeserializeObject<ItemInspecao[]>(GetArquivoSeed("item_inspecao"));
            var grupos = ItemInspecoes.GroupBy(g => g.Grupo.Descricao);
            foreach (var item in grupos)
            {
                if (!context.ItensInspecao.Any(w => w.Grupo.Descricao.ToLower() == item.Key.ToLower()))
                {
                    var itens = item.ToList();
                    foreach (var inspecao in itens)
                    {
                        inspecao.Grupo = context.Grupos.FirstOrDefault(a => a.Descricao.ToLower() == item.Key.ToLower());
                    }
                    context.ItensInspecao.AddRange(itens);
                    context.SaveChanges();
                }
            }

        }


        // private static void SeedVersao(BancoContext context)
        // {
        //     if (context.Versoes.Any())
        //         return;

        //     context.Versoes.Add(new Versao
        //     {
        //         Plataforma = Enum.TipoDispositivo.Android,
        //         VersaoAtual = "1.3.50"
        //     });

        //     context.Versoes.Add(new Versao
        //     {
        //         Plataforma = Enum.TipoDispositivo.IOS,
        //         VersaoAtual = "1.3.50"
        //     });

        //     context.SaveChanges();
        // }

        private static string GetArquivoSeed(string nomeTabela)
        {
            return File.ReadAllText(@"DbSeed" + Path.DirectorySeparatorChar + nomeTabela + ".json");
        }

        private static void SeedGrupos(BancoContext context)
        {
            if (context.Grupos.Any())
                return;
            Grupo[] grupos = JsonConvert
                            .DeserializeObject<Grupo[]>(GetArquivoSeed(nameof(context.Grupos).ToLower()));
            context.Grupos.AddRange(grupos);
            context.SaveChanges();
        }
        private static void SeedUsuario(BancoContext context)
        {
            if (context.Usuarios.Any())
                return;
            Usuario usuario = JsonConvert
                            .DeserializeObject<Usuario>(GetArquivoSeed(nameof(context.Usuarios).ToLower()));
            context.Usuarios.Add(usuario);
            context.SaveChanges();

            context.PerfilUsuarios.Add(new PerfilUsuario(){
                UsuarioId = usuario.Id,
            });
            context.SaveChanges();
        }
        private static void SeedCliente(BancoContext context)
        {
            if (context.Clientes.Any())
                return;
            Cliente cliente = JsonConvert
                            .DeserializeObject<Cliente>(GetArquivoSeed(nameof(context.Clientes).ToLower()));
            context.Clientes.Add(cliente);
            context.SaveChanges();
        }
    }
}
