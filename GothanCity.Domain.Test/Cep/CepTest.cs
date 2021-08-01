using FluentAssertions;
using GothamCity.Domain.Commands.Cep;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GothamCity.Domain.Test.CepTest
{

    public class CepTest
    {
        [Test]
        public async Task AO_EXECUTAR_HANDLE_DO_ADICIONAR_CEP_DADO_QUE_O_CEP_E_VALIDO_E_QUE_O_FLUXO_OCORRE_NORMALMENTE_ESPERASE_QUE_SEJA_VALIDADO()
        {
            //arrange
            var cepAdicionarResponse = new Entities.Cep("123456", "Maringá-PR");
            var mediatorMock = new Mock<MediatR.IMediator>();
            mediatorMock.Setup(s => s.Publish(It.IsAny<AdicionarCepNotification>(), default));
            var repositoryCepMock = new Mock<Interfaces.Repositories.IRepositoryCep>();
            repositoryCepMock.Setup(s => s.Adicionar(It.IsAny<Entities.Cep>())).Returns(cepAdicionarResponse);

            var adicionarCepHandler = new AdicionarCepHandler(mediatorMock.Object, repositoryCepMock.Object);
            var adicionarCepRequest = new AdicionarCepRequest() { cidade = "Maringá-PR", codigoCep = "123456" };

            //act
            var response = await adicionarCepHandler.Handle(adicionarCepRequest, default);

            //assert
            response.Data.Should().BeSameAs(cepAdicionarResponse);
            response.Mensage.Should().BeNullOrEmpty();
            response.Success.Should().BeTrue();
            mediatorMock.Verify(x => x.Publish(It.IsAny<AdicionarCepNotification>(), default), Times.Once);
            repositoryCepMock.Verify(x => x.Adicionar(It.IsAny<Entities.Cep>()), Times.Once);
        }

        [Test]
        public async Task AO_EXECUTAR_HANDLE_DO_ADICIONAR_CEP_DADO_QUE_O_REQUEST_SEJA_NULO_E_QUE_O_FLUXO_OCORRE_NORMALMENTE_ESPERASE_UMA_MENSAGEM_DE_VALIDACAO_SOLICITANDO_O_CEP()
        {
            //arrange
            var mediatorMock = new Mock<MediatR.IMediator>();
            var repositoryCepMock = new Mock<Interfaces.Repositories.IRepositoryCep>();
            var adicionarCepHandler = new AdicionarCepHandler(mediatorMock.Object, repositoryCepMock.Object);

            //act
            var response = await adicionarCepHandler.Handle(null, default);

            //assert
            response.Data.Should().BeOfType<DateTime>();
            response.Mensage.Should().Be("Informe os dados do cep.");
            response.Success.Should().BeFalse();
            mediatorMock.Verify(x => x.Publish(It.IsAny<AdicionarCepNotification>(), default), Times.Never);
            repositoryCepMock.Verify(x => x.Adicionar(It.IsAny<Entities.Cep>()), Times.Never);
        }

        [Test]
        public async Task AO_EXECUTAR_HANDLE_DO_ADICIONAR_CEP_DADO_QUE_O_CEP_JA_EXISTA_E_QUE_O_FLUXO_OCORRE_NORMALMENTE_ESPERASE_UMA_MENSAGEM_DE_VALIDACAO_INFORMANDO_QUE_JA_EXISTE_O_CEP()
        {
            //arrange
            var mediatorMock = new Mock<MediatR.IMediator>();
            mediatorMock.Setup(s => s.Publish(It.IsAny<AdicionarCepNotification>(), default));
            var repositoryCepMock = new Mock<Interfaces.Repositories.IRepositoryCep>();
            repositoryCepMock.Setup(s => s.Existe(It.IsAny<Func<Entities.Cep, bool>>())).Returns(true);

            var adicionarCepHandler = new AdicionarCepHandler(mediatorMock.Object, repositoryCepMock.Object);
            var adicionarCepRequest = new AdicionarCepRequest() { cidade = "Maringá-PR", codigoCep = "123456" };

            //act
            var response = await adicionarCepHandler.Handle(adicionarCepRequest, default);

            //assert
            response.Data.Should().BeOfType<DateTime>();
            response.Mensage.Should().Be("Cep já cadastrado.");
            response.Success.Should().BeFalse();
            mediatorMock.Verify(x => x.Publish(It.IsAny<AdicionarCepNotification>(), default), Times.Never);
            repositoryCepMock.Verify(x => x.Adicionar(It.IsAny<Entities.Cep>()), Times.Never);
        }

        [Test]
        public async Task AO_EXECUTAR_HANDLE_DO_ADICIONAR_CEP_DADO_QUE_O_CEP_SEJA_MENOR_QUE_100000_FLUXO_OCORRE_NORMALMENTE_ESPERASE_UMA_MENSAGEM_DE_VALIDACAO_INFORMANDO_QUE_NAO_PODE_SER_MENOR_QUE_100000()
        {
            //arrange
            var mediatorMock = new Mock<MediatR.IMediator>();
            mediatorMock.Setup(s => s.Publish(It.IsAny<AdicionarCepNotification>(), default));
            var repositoryCepMock = new Mock<Interfaces.Repositories.IRepositoryCep>();
            repositoryCepMock.Setup(s => s.Existe(It.IsAny<Func<Entities.Cep, bool>>())).Returns(true);

            var adicionarCepHandler = new AdicionarCepHandler(mediatorMock.Object, repositoryCepMock.Object);
            var adicionarCepRequest = new AdicionarCepRequest() { cidade = "Maringá-PR", codigoCep = "99999" };

            //act
            var response = await adicionarCepHandler.Handle(adicionarCepRequest, default);

            //assert
            response.Data.Should().BeOfType<DateTime>();
            response.Mensage.Should().Be("O cep informado precisa ser maior que 100000.");
            response.Success.Should().BeFalse();
            mediatorMock.Verify(x => x.Publish(It.IsAny<AdicionarCepNotification>(), default), Times.Never);
            repositoryCepMock.Verify(x => x.Adicionar(It.IsAny<Entities.Cep>()), Times.Never);
        }

        [Test]
        public async Task AO_EXECUTAR_HANDLE_DO_ADICIONAR_CEP_DADO_QUE_O_CEP_SEJA_MAIOR_QUE_999999_FLUXO_OCORRE_NORMALMENTE_ESPERASE_UMA_MENSAGEM_DE_VALIDACAO_INFORMANDO_QUE_NAO_PODE_SER_MAIOR_QUE_999999()
        {
            //arrange
            var mediatorMock = new Mock<MediatR.IMediator>();
            mediatorMock.Setup(s => s.Publish(It.IsAny<AdicionarCepNotification>(), default));
            var repositoryCepMock = new Mock<Interfaces.Repositories.IRepositoryCep>();
            repositoryCepMock.Setup(s => s.Existe(It.IsAny<Func<Entities.Cep, bool>>())).Returns(true);

            var adicionarCepHandler = new AdicionarCepHandler(mediatorMock.Object, repositoryCepMock.Object);
            var adicionarCepRequest = new AdicionarCepRequest() { cidade = "Maringá-PR", codigoCep = "1000000" };

            //act
            var response = await adicionarCepHandler.Handle(adicionarCepRequest, default);

            //assert
            response.Data.Should().BeOfType<DateTime>();
            response.Mensage.Should().Be("O cep informado precisa ser menor que 999999.");
            response.Success.Should().BeFalse();
            mediatorMock.Verify(x => x.Publish(It.IsAny<AdicionarCepNotification>(), default), Times.Never);
            repositoryCepMock.Verify(x => x.Adicionar(It.IsAny<Entities.Cep>()), Times.Never);
        }

        [Test]
        public async Task AO_EXECUTAR_HANDLE_DO_ADICIONAR_CEP_DADO_QUE_O_CEP_TENHA_DIGITO_REPETITIVO_ALTERNADO_EM_PARES_FLUXO_OCORRE_NORMALMENTE_ESPERASE_UMA_MENSAGEM_DE_VALIDACAO_INFORMANDO_QUE_NAO_PODE_TER_NUMEROS_REPETIDIVO_ALTERNADO_EM_PAR()
        {
            //arrange
            var mediatorMock = new Mock<MediatR.IMediator>();
            mediatorMock.Setup(s => s.Publish(It.IsAny<AdicionarCepNotification>(), default));
            var repositoryCepMock = new Mock<Interfaces.Repositories.IRepositoryCep>();
            repositoryCepMock.Setup(s => s.Existe(It.IsAny<Func<Entities.Cep, bool>>())).Returns(true);

            var adicionarCepHandler = new AdicionarCepHandler(mediatorMock.Object, repositoryCepMock.Object);
            var adicionarCepRequest = new AdicionarCepRequest() { cidade = "Maringá-PR", codigoCep = "552523" };

            //act
            var response = await adicionarCepHandler.Handle(adicionarCepRequest, default);

            //assert
            response.Data.Should().BeOfType<DateTime>();
            response.Mensage.Should().Be("O cep informado não pode conter nenhum dígito repetitivo alternado em par.");
            response.Success.Should().BeFalse();
            mediatorMock.Verify(x => x.Publish(It.IsAny<AdicionarCepNotification>(), default), Times.Never);
            repositoryCepMock.Verify(x => x.Adicionar(It.IsAny<Entities.Cep>()), Times.Never);
        } 
    }
}
