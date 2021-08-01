using System;
using System.Collections.Generic;
using System.Text;

namespace GothamCityTest.Helper
{
    public static class CepHelper
    {
        public static bool validardigitorepetitivoalternado(string cep)
        {
            bool valido = false;
            if (cep.Length == 6
                && cep[0] != cep[2]
                && cep[1] != cep[3]
                && cep[2] != cep[4]
                && cep[3] != cep[5])
                valido = true;

            return valido;
        }

    //    public static bool validardigito(string cep)
    //    {
    //        //cep = String.sub(r"[^\d]", "", cep)
    //        bool[] valid;
    //if (cep.Length == 6)
    //    for (int i = 0; i <= 4; i++)
    //        if (cep[i] != cep[i + 2])
    //            valid.append(True)
    //        else
    //            valid.append(False)
    //return all(valid or[False])
    //    }
    }
}
