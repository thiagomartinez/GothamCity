using GothamCity.Domain.Interfaces.Repositories;
using GothamCity.Domain.Entities;
using GothamCity.Infra.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;

namespace GothamCity.Infra.Repository
{
    public class RepositoryCep : RepositoryBase<Cep, Guid>, IRepositoryCep
    {
        private readonly GothamCityContext _context;
        public RepositoryCep(GothamCityContext context) : base(context)
        {
            _context = context;
        }
    }
}
