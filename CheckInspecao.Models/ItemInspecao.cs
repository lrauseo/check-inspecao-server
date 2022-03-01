using System.Collections.Generic;

namespace CheckInspecao.Models
{
    public class ItemInspecao
    {
        public int Id { get; set; }

        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }
        public string Classificacao { get; set; }
        public string Descricao { get; set; }        
    }
}