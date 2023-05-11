using MediatR;
using Moq;
using SistemaCompra.Domain.Core;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using SistemaCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandlerTests
    {
        private RegistrarCompraCommandHandler _handler;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMediator> _mediatorMock;
        private Mock<SistemaCompraAgg.ISolicitacaoCompraRepository> _repositoryMock;

        public RegistrarCompraCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mediatorMock = new Mock<IMediator>();
            _repositoryMock = new Mock<SistemaCompraAgg.ISolicitacaoCompraRepository>();

            _handler = new RegistrarCompraCommandHandler(
                _unitOfWorkMock.Object,
                _mediatorMock.Object,
                _repositoryMock.Object
            );
        }

        [Fact]
        public async Task Handle_ValidCommand_RegistersCompraAndCommitsUnitOfWork()
        {
            // Arrange
            var command = new RegistrarCompraCommand
            {
                UsuarioSolicitante = "UserParaTeste",
                NomeFornecedor = "FornecedorParaTeste"
            };

            var solicitacaoCompra = new SistemaCompraAgg.SolicitacaoCompra("UserParaTeste", "FornecedorParaTeste");

            _repositoryMock.Setup(x => x.RegistrarCompra(It.IsAny<SistemaCompraAgg.SolicitacaoCompra>()))
                .Callback<SistemaCompraAgg.SolicitacaoCompra>(x => solicitacaoCompra = x);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(x => x.RegistrarCompra(It.IsAny<SistemaCompraAgg.SolicitacaoCompra>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);

            Assert.True(result);
            Assert.Equal(command.UsuarioSolicitante, solicitacaoCompra.UsuarioSolicitante.Nome);
            Assert.Equal(command.NomeFornecedor, solicitacaoCompra.NomeFornecedor.Nome);
        }

        [Fact]
        public void Handle_RepositoryThrowsBusinessRuleException_ThrowsBusinessRuleException()
        {
            // Arrange
            var command = new RegistrarCompraCommand
            {
                UsuarioSolicitante = "UserParaTeste",
                NomeFornecedor = "FornecedorParaTeste"
            };

            _repositoryMock.Setup(x => x.RegistrarCompra(It.IsAny<SistemaCompraAgg.SolicitacaoCompra>()))
                .Throws(new BusinessRuleException("Erro ao criar Solicitação de compra."));

            // Act
            // Assert
            Assert.ThrowsAsync<BusinessRuleException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public void Handle_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var command = new RegistrarCompraCommand
            {
                UsuarioSolicitante = "UserParaTeste",
                NomeFornecedor = "FornecedorParaTeste"
            };

            _repositoryMock.Setup(x => x.RegistrarCompra(It.IsAny<SistemaCompraAgg.SolicitacaoCompra>()))
                .Throws(new Exception("Erro ao criar Solicitação de compra."));

            // Act
            // Assert
            Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}