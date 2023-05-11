using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models = SistemaCompra.Domain.Core.Model;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;

namespace SistemaCompra.Infra.Data.Produto
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<ProdutoAgg.Produto>
    {
        public void Configure(EntityTypeBuilder<ProdutoAgg.Produto> builder)
        {
            builder.ToTable("Produto");
            builder.Property(p => p.Preco)
                   .HasConversion(
                        preco => preco.Value,
                        value => new Models.Money(value))
                .HasColumnName("Preco");
        }
    }
}
