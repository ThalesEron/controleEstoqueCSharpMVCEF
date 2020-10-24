using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleEstoque
{
    public class HistoricoMap
    {
        public class ProdutoMap : IEntityTypeConfiguration<Historico>
        {

            public void Configure(EntityTypeBuilder<Historico> builder)
            {
                builder.HasKey(t => t.Id);
                builder.Property(t => t.Nome)
                    .HasMaxLength(50)
                    .IsRequired();
                builder.Property(t => t.Quantidade)
                    .IsRequired();
                builder.Property(t => t.Data)
                    .IsRequired();
                builder.Property(t => t.Funcao)
                    .HasMaxLength(20)
                    .IsRequired();

                builder.ToTable(nameof(Historico));




            }

        }
    }
}
