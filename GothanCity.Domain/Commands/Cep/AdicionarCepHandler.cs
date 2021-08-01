using GothamCity.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GothamCity.Domain.Helpers;

namespace GothamCity.Domain.Commands.Cep
{
    public class AdicionarCepHandler : IRequestHandler<AdicionarCepRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryCep _repositoryCep;

        public AdicionarCepHandler(IMediator mediator, IRepositoryCep repositoryCep)
        {
            _mediator = mediator;
            _repositoryCep = repositoryCep;
        }

        public async Task<Response> Handle(AdicionarCepRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new Response("Informe os dados do cep.", false, DateTime.Now);
            }

            if (!String.IsNullOrEmpty(request.codigoCep) && (Convert.ToInt32(request.codigoCep) < 100000))
            {
                return new Response("O cep informado precisa ser maior que 100000.", false, DateTime.Now);
            }

            if (!String.IsNullOrEmpty(request.codigoCep) && (Convert.ToInt32(request.codigoCep) > 999999))
            {
                return new Response("O cep informado precisa ser menor que 999999.", false, DateTime.Now);
            }

            CepHelper help = new CepHelper();
            if (!String.IsNullOrEmpty(request.codigoCep) && help.validarDigitoRepetitivoAlternado(request.codigoCep))
            {
                return new Response("O cep informado não pode conter nenhum dígito repetitivo alternado em par.", false, DateTime.Now);
            }

            if (_repositoryCep.Existe(x=>x.CodigoCep == request.codigoCep))
            {
                return new Response("Cep já cadastrado.", false, DateTime.Now);
            }

            Entities.Cep cep = new Entities.Cep(request.codigoCep, request.cidade);
            cep = _repositoryCep.Adicionar(cep);
            var response = new Response("", true, cep);

            AdicionarCepNotification adicionarCepNotification = new AdicionarCepNotification(cep);

            await _mediator.Publish(adicionarCepNotification);

            return await Task.FromResult(response);
        }
    }
}
