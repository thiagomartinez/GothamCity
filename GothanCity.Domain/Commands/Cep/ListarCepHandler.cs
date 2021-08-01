using GothamCity.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GothamCity.Domain.Commands.Cep
{
    public class ListarCepHandler : IRequestHandler<ListarCepRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryCep _repositoryCep;

        public ListarCepHandler(IMediator mediator, IRepositoryCep repositoryCep)
        {
            _mediator = mediator;
            _repositoryCep = repositoryCep;
        }

        public async Task<Response> Handle(ListarCepRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new Response("Informe os dados do cep.", false, DateTime.Now);
            }

            var cepCollection = _repositoryCep.Listar().ToList();

            var response = new Response("", true, cepCollection);

            return await Task.FromResult(response);
        }
    }
}
