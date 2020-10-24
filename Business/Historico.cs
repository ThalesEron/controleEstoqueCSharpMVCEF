using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Historico
    {

        public int Id { get; set; }
        public string Nome { get; set; }

        public int Quantidade { get; set; }

        public DateTime Data { get; set; }

        public string Funcao { get; set; }

    }
}
