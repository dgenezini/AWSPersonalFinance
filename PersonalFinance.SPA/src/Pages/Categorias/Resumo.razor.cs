using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.CustomExtensions;
using PersonalFinance.Domain.Values;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PersonalFinance.Pages.Categorias
{
    [Authorize]
    public partial class Resumo : ComponentBase
    {
        public ResumoMensal ResumoMensal { get; set; }
        public bool UsaDataPagamento { get; set; }

        [Inject]
        private HttpClient _HttpClient { get; set; }
        [Inject]
        NavigationManager _NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var Periodo = _NavigationManager.QueryString("Periodo");

            ResumoMensal = await _HttpClient
                .GetFromJsonAsync<ResumoMensal>($"Resumo?Periodo={Periodo}");
        }
    }
}
