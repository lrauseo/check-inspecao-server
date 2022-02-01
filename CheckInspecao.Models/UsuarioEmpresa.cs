using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInspecao.Models
{
    public class UsuarioEmpresa
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
