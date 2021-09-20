using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSL.Cadastro.Dominio.Models.ViewModels
{
    public class EntregaViewModel
    {
        public string NomeCliente { get; set; }
        public string NomeMercadoria { get; set; }
        public string EnderecoDeposito { get; set; }
        public string EnderecoCliente { get; set; }
        public string PrevisaoEntrega { get; set; }
    }
}
