using System;
using System.Collections.Generic;

namespace CheckInspecao.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataCadastro { get; set; }                
        // public Login Login { get; set; }
        // public Empresa Empresa { get; set; }
        public IList<DocumentoInspecao> Documentos { get; set; }

    }
}