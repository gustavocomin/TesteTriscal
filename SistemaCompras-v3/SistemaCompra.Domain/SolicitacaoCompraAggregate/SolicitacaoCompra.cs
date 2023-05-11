﻿using SistemaCompra.Domain.Core;
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
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            ValidaItens(itens);
            Itens = itens.ToList();
            AtualizaTotalGeral();
            ValidarAtualizacaoPagTotCompra();
        }

        public void ValidarAtualizacaoPagTotCompra()
        {
            if (TotalGeral != null && TotalGeral.Value > 5000)
                CondicaoPagamento = new CondicaoPagamento(30);
        }

        public void AtualizaTotalGeral()
        {
            decimal produtoTotal = 0;
            List<int> quantidades = Itens.Select(x => x.Qtde).ToList();
            List<decimal> valores = Itens.Select(x => x.Produto.Preco.Value).ToList();

            for (int i = 0; i < Itens.Count; i++)
            {
                decimal produto = quantidades[i] * valores[i];
                produtoTotal += produto;
            }

            TotalGeral = new Money(produtoTotal);
        }

        public void ValidaItens(IEnumerable<Item> itens)
        {
            if (itens == null || !itens.Any())
                throw new BusinessRuleException("A solicitação de compra deve possuir itens!");
        }
    }
}
