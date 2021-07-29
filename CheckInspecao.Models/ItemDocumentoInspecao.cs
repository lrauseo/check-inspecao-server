using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CheckInspecao.Models
{
    public class ItemDocumentoInspecao:IEqualityComparer<ItemDocumentoInspecao>
    {
        public int Id { get; set; }
        public DocumentoInspecao Documento { get; set; }
        public ItemInspecao Item { get; set; }
        public bool Sim { get; set; }
        public bool Nao { get; set; }
        public bool NaoSeAplica { get; set; }
        public bool NaoObservado { get; set; }
        public string Observacao { get; set; }
        public IList<Foto> Fotos { get; set; }

        public bool Equals(ItemDocumentoInspecao x, ItemDocumentoInspecao y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ItemDocumentoInspecao obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}