using SistemaCompra.Domain.Core;
using System;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class NomeFornecedor
    {
        public string Nome { get; }
        public Guid Id { get; set; }

        private NomeFornecedor()
        {
        }

        public NomeFornecedor(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentNullException(nameof(nome));
            if (nome.Length < 10) throw new BusinessRuleException("Nome de fornecedor deve ter pelo menos 10 caracteres.");

            Nome = nome;
        }
    }
}
