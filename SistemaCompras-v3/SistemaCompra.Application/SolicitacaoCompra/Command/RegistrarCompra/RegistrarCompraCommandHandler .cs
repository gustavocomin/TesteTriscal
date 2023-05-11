using MediatR;
using SistemaCompra.Domain.Core;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Threading;
using System.Threading.Tasks;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly SolicitacaoAgg.ISolicitacaoCompraRepository _repository;

        public RegistrarCompraCommandHandler(IUnitOfWork uow, IMediator mediator, SolicitacaoAgg.ISolicitacaoCompraRepository repository) : base(uow, mediator)
        {
            _repository = repository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {

            try
            {
                SolicitacaoAgg.SolicitacaoCompra solicitacaoCompra = new SolicitacaoAgg.SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor);
                _repository.RegistrarCompra(solicitacaoCompra);

                Commit();
                PublishEvents(solicitacaoCompra.Events);
            }
            catch (BusinessRuleException e)
            {
                throw new BusinessRuleException($"Erro ao criar Solicitação de compra. Erro: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao criar Solicitação de compra. Erro: {e.Message}");
            }

            return Task.FromResult(true);
        }
    }
}