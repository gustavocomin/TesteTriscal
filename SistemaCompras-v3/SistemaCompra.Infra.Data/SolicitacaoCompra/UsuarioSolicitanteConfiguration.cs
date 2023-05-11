using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SolitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class UsuarioSolicitanteConfiguration : IEntityTypeConfiguration<SolitacaoCompraAgg.UsuarioSolicitante>
    {
        public void Configure(EntityTypeBuilder<UsuarioSolicitante> builder)
        {
            builder.ToTable("UsuarioSolicitante");

            builder.Property(u => u.Id)
                   .IsRequired();

            builder.Property(u => u.Nome)
                   .IsRequired();
        }
    }
}