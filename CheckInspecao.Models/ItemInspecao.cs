using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CheckInspecao.Models
{
    public class ItemInspecao : IEqualityComparer<ItemInspecao>
    {
        public int Id { get; set; }

        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }
        public string Classificacao { get; set; }
        public string Descricao { get; set; }     
        public IList<ItemInspecaoQuestionarioFormulario> Questionarios { get; set; }

        public bool Equals(ItemInspecao x, ItemInspecao y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ItemInspecao obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}