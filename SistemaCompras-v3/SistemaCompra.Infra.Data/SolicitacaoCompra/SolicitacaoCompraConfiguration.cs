using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models = SistemaCompra.Domain.Core.Model;
using SolitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolitacaoCompraAgg.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolitacaoCompraAgg.SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompra");

            builder.Property(p => p.Data)
                   .IsRequired();

            builder.Property(p => p.Situacao)
                   .IsRequired();

            builder.Property(p => p.IdUsuarioSolicitante)
                   .IsRequired();

            builder.Property(p => p.IdNomeFornecedor)
                   .IsRequired();

            builder.Property(p => p.IdCondicaoPagamento)
                   .IsRequired();

            builder.Property(p => p.TotalGeral)
                   .HasConversion(
                        totalGeral => totalGeral.Value,
                        value => new Models.Money(value))
                   .IsRequired();

            builder.HasOne(d => d.NomeFornecedor)
                   .WithMany()
                   .HasForeignKey(d => d.Id)
                   .IsRequired();

            builder.HasOne(d => d.UsuarioSolicitante)
                   .WithMany()
                   .HasForeignKey(d => d.Id)
                   .IsRequired();

            builder.HasOne(d => d.CondicaoPagamento)
                   .WithMany()
                   .HasForeignKey(d => d.Id)
                   .IsRequired();

            builder.HasOne(d => d.Itens)
                   .WithMany()
                   .HasForeignKey(d => d.Id)
                   .IsRequired();
        }
    }
}