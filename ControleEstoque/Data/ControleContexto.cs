
using Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleEstoque
{
    public class ControleContexto : DbContext
    {

        public ControleContexto(DbContextOptions<ControleContexto> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Historico> Historicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMap());
        }

    }
}
