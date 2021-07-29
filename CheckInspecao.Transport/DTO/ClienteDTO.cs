using System;
using System.Collections.Generic;

namespace CheckInspecao.Transport.DTO
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataCadastro { get; set; }
        // public LoginDTO Login { get; set; }
        // public EmpresaDTO Empresa { get; set; }
        // public IList<DocumentoInspecaoDTO> Documentos { get; set; }
    }
}