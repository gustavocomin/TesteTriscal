using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SolitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class CondicaoPagamentoConfiguration : IEntityTypeConfiguration<SolitacaoCompraAgg.CondicaoPagamento>
    {
        public void Configure(EntityTypeBuilder<CondicaoPagamento> builder)
        {
            builder.ToTable("CondicaoPagamento");

            builder.Property(c => c.Id)
                   .IsRequired();

            builder.Property(c => c.Valor)
                   .IsRequired();
        }
    }
}