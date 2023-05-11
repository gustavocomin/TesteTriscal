using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }

        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public Guid IdUsuarioSolicitante { get; private set; }

        public NomeFornecedor NomeFornecedor { get; private set; }
        public Guid IdNomeFornecedor { get; private set; }

        public CondicaoPagamento CondicaoPagamento { get; private set; }
        public Guid IdCondicaoPagamento { get; private set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
            ValidaTotalGeral(this);
            AtualizaCondPagto(this);
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {

        }

        public void AtualizaCondPagto(SolicitacaoCompra solicitacaoCompra)
        {
            if (solicitacaoCompra.TotalGeral.Value > 5000)
                solicitacaoCompra.CondicaoPagamento = new CondicaoPagamento(30);
        }

        public void ValidaTotalGeral(SolicitacaoCompra solicitacaoCompra)
        {
            if (!solicitacaoCompra.Itens.ToList().Any())
                throw new Exception("Total de intes deve ser maior que zero");
        }
    }
}
