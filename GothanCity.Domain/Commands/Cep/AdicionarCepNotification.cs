using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GothamCity.Domain.Commands.Cep
{
    public class  AdicionarCepNotification : INotification
    {
        public Entities.Cep Cep { get; set; }

        public AdicionarCepNotification(Entities.Cep cep)
        {
            Cep = cep;
        }
    }
}
