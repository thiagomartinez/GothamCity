using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GothamCity.Domain.Commands.Cep
{
    public class AdicionarCepRequest : IRequest<Response>
    {
        public string codigoCep { get; set; }
        public string cidade { get; set; }
    }
}
