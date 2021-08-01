using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GothamCity.Test.Cep
{
    [TestClass]
    public class CepTest
    {
        [TestMethod]
        public void Cep_CriarCep_RetornaCep() 
        {
            var CepEsperado = new { Cep = "100000", Cidade = "Maringá" };

            var cep = new Cep(CepEsperado.Cep, CepEsperado.Cidade);

            Assert.Equals(CepEsperado.Cep, cep.Cep);
            Assert.Equals(CepEsperado.Cidade, cep.Cidade);
        }

    }
}
