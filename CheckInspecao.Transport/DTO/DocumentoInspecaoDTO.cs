using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckInspecao.Transport.DTO
{
    public class DocumentoInspecaoDTO
    {
        public int Id { get; set; }
        public DateTime DataDocumento { get; set; }
        public List<ItemDocumentoInspecaoDTO> Itens { get; set; }        
        
        public UsuarioDTO UsuarioInspecao { get; set; }        
        
        public ClienteDTO Cliente { get; set; }
    }
}