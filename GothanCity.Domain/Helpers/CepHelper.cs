using System;
using System.Collections.Generic;
using System.Text;

namespace GothamCity.Domain.Helpers
{
    public class CepHelper
    {
        public bool validarDigitoRepetitivoAlternado(string cep)
        {
            bool valid = false;
            if (cep.Length == 6)
                for (int i = 0; i <= 3; i++)
                    if (cep[i] == cep[i + 2] && !valid)
                        valid = true;
            return valid;
        }
    }
}
