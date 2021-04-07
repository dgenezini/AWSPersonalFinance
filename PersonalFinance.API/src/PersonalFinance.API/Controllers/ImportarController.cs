
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.API.APIModels;
using PersonalFinance.Application.Interactors;
using System.Threading.Tasks;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportarController : ControllerBase
    {
        private readonly ImportarInteractor _ImportarInteractor;

        public ImportarController(ImportarInteractor importarInteractor)
        {
            _ImportarInteractor = importarInteractor;
        }

        [HttpPost]
        public async Task<bool> PostAsync(ImportarRequest importarRequest)
        {
            await _ImportarInteractor
                .ImportarAsync(importarRequest.ImportacaoTipo, importarRequest.Arquivo);

            return true;
        }
    }
}
