using GothamCity.Infra.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GothamCity.Infra.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GothamCityContext _context;

        public UnitOfWork(GothamCityContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
