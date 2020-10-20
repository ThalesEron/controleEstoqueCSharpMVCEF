
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class Produto 
    {


        public int Id { get; set; }
        public string Nome { get; set; }

        public int Quantidade { get; set; }







    }
}
