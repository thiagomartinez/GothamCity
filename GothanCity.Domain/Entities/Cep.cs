using System;

namespace GothamCity.Domain.Entities
{
    public class Cep : EntityBase
    {
        public String CodigoCep { get; private set; }
        public String Cidade { get; private set; }

        protected Cep()
        {

        }

        public Cep(String codigoCep, String cidade)
        {
            this.CodigoCep = codigoCep;
            this.Cidade = cidade;
        }
    }
}
