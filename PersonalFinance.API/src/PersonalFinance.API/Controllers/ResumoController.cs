using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Application.Interactors;
using PersonalFinance.Domain.Values;
using System;
using System.Threading.Tasks;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResumoController : ControllerBase
    {
        private readonly GetResumoInteractor _GetResumoInteractor;

        public ResumoController(GetResumoInteractor getResumoInteractor)
        {
            _GetResumoInteractor = getResumoInteractor;
        }

        [HttpGet]
        public async Task<ResumoMensal> GetAsync(DateTime? periodo = null)
        {
            return await _GetResumoInteractor.GetResumoAsync(periodo);
        }
    }
}
