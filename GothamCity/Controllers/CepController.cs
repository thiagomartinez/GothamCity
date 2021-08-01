using GothamCity.Domain.Commands.Cep;
using GothamCity.Infra.Transactions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace GothamCity.Api.Controllers
{
    public class CepController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CepController(IMediator mediator, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var request = new ListarCepRequest();
                var result = await _mediator.Send(request, CancellationToken.None);
                return View(result.Data);
            }
            catch (System.Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(AdicionarCepRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _mediator.Send(request, CancellationToken.None);
                    await ResponseAsync(response);
                    return Redirect(nameof(Index));
                }
                return View();
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Cep/Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var request = new ListarCepRequest();
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (System.Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Cep/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarCepRequest request)
        {
            try
            {
                var response = await _mediator.Send(request, CancellationToken.None);
                return await ResponseAsync(response);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
