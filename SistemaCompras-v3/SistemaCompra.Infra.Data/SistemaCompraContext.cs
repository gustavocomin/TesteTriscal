using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaCompra.Domain.Core;
using SistemaCompra.Infra.Data.Produto;
using System;
using System.Collections.Generic;
using Models = SistemaCompra.Domain.Core.Model;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SolicComprasAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data
{
    public class SistemaCompraContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public SistemaCompraContext()
        {
        }

        public SistemaCompraContext(DbContextOptions options) : base(options) { }
        public DbSet<ProdutoAgg.Produto> Produtos { get; set; }
        public DbSet<SolicComprasAgg.SolicitacaoCompra> SolicitacaoCompra { get; set; }
        public DbSet<SolicComprasAgg.UsuarioSolicitante> UsuarioSolicitante { get; set; }
        public DbSet<SolicComprasAgg.NomeFornecedor> NomeFornecedor { get; set; }
        public DbSet<SolicComprasAgg.CondicaoPagamento> CondicaoPagamento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            decimal precoValue = 100m;
            string nome = "UserParaTeste";
            int condicao = 30;

            modelBuilder.Entity<Models.Money>()
                .HasNoKey()
                .ToTable("Money")
                .Property<decimal>("Value");

            modelBuilder.Entity<Models.Money>()
                .HasData(new Models.Money(precoValue));

            modelBuilder.Entity<ProdutoAgg.Produto>()
                .HasData(new
                {
                    Id = Guid.NewGuid(),
                    Nome = "Produto01",
                    Descricao = "Descricao01",
                    Categoria = ProdutoAgg.Categoria.Madeira,
                    Situacao = ProdutoAgg.Situacao.Ativo,
                    Preco = new Models.Money(precoValue)
                });

            modelBuilder.Entity<SolicComprasAgg.UsuarioSolicitante>()
                .HasData(new
                {
                    Id = Guid.NewGuid(),
                    Nome = nome
                });

            modelBuilder.Entity<SolicComprasAgg.NomeFornecedor>()
                .HasData(new
                {
                    Id = Guid.NewGuid(),
                    Nome = nome
                });

            modelBuilder.Entity<SolicComprasAgg.CondicaoPagamento>()
                .HasData(new
                {
                    Id = Guid.NewGuid(),
                    Valor = condicao
                });

            var solicitacaoCompraId = Guid.NewGuid();
            var condicaoPagamentoId = Guid.NewGuid();
            var nomeFornecedorId = Guid.NewGuid();
            var usuarioSolicitanteId = Guid.NewGuid();

            modelBuilder.Entity<SolicComprasAgg.SolicitacaoCompra>()
                .HasData(new
                {
                    Id = solicitacaoCompraId,
                    Data = DateTime.Today,
                    Situacao = SolicComprasAgg.Situacao.Solicitado,
                    TotalGeral = new Models.Money(precoValue),
                    Itens = new List<SolicComprasAgg.Item>(),
                    IdCondicaoPagamento = condicaoPagamentoId,
                    IdUsuarioSolicitante = usuarioSolicitanteId,
                    IdNomeFornecedor = nomeFornecedorId
                });

            modelBuilder.Entity<SolicComprasAgg.CondicaoPagamento>()
                .HasData(new
                {
                    Id = condicaoPagamentoId,
                    Valor = condicao
                });

            modelBuilder.Entity<SolicComprasAgg.UsuarioSolicitante>()
                .HasData(new
                {
                    Id = usuarioSolicitanteId,
                    Nome = nome
                });

            modelBuilder.Entity<SolicComprasAgg.NomeFornecedor>()
                .HasData(new
                {
                    Id = nomeFornecedorId,
                    Nome = nome
                });

            modelBuilder.Ignore<Models.Money>();
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
        }


        public bool TestarConexao()
        {
            return Database.CanConnect();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer(@"Server=Desktop-Gustavo\SQLEXPRESS;Database=SistemaCompraDb;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=Gustavo;Password=12345");
        }
    }
}
