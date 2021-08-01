using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GothamCity.Domain.Commands
{
    public class Response
    {
        public Response(String mensage, bool success, object data)
        {
            Mensage = mensage;
            Success = success;
            Data = data;
        }

        public String Mensage { get; private set; }
        public bool Success { get; private set; }
        public object Data { get; private set; }
    }
}
