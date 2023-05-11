using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SolitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class NomeFornecedorConfiguration : IEntityTypeConfiguration<SolitacaoCompraAgg.NomeFornecedor>
    {
        public void Configure(EntityTypeBuilder<NomeFornecedor> builder)
        {
            builder.ToTable("NomeFornecedor");

            builder.Property(u => u.Id)
                   .IsRequired();

            builder.Property(u => u.Nome)
                   .IsRequired();
        }
    }
}