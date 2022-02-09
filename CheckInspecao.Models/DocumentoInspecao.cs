using System;
using System.Collections.Generic;

namespace CheckInspecao.Models
{
    public class DocumentoInspecao
    {
        public int Id { get; set; }
        public DateTime DataDocumento { get; set; }
        public List<ItemDocumentoInspecao> Itens { get; set; }
        public int PerfilUsuarioId { get; set; }
        public PerfilUsuario UsuarioInspecao { get; set; }
        public Cliente Cliente { get; set; }
    }
}