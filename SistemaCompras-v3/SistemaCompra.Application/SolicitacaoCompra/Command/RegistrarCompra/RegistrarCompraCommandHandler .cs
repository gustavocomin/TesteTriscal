using MediatR;
using SistemaCompra.Infra.Data.UoW;
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
            SolicitacaoAgg.SolicitacaoCompra solicitacaoCompra = new SolicitacaoAgg.SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor);
            _repository.RegistrarCompra(solicitacaoCompra);

            Commit();
            PublishEvents(solicitacaoCompra.Events);

            return Task.FromResult(true);
        }
    }
}