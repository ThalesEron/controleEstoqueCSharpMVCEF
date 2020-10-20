using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleEstoque
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {

        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Nome)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(t => t.Quantidade)
                .IsRequired();

            builder.ToTable(nameof(Produto));




        }

    }
}
