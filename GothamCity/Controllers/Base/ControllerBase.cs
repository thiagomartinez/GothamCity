using GothamCity.Domain.Commands;
using GothamCity.Infra.Transactions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GothamCity.Api.Controllers
{
    public class ControllerBase : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ControllerBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> ResponseAsync(Response response)
        {

            try
            {
                _unitOfWork.SaveChanges();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema interno com o servidor. Entre em contato com o Administrador do sistema caso o problema continue.");
            }

        }
    }
}
