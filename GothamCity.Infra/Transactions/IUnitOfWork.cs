using System;
using System.Collections.Generic;
using System.Text;

namespace GothamCity.Infra.Transactions
{    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}
