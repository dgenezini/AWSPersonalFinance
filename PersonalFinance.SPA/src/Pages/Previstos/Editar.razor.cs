using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PersonalFinance.Pages.Previstos
{
    [Authorize]
    public partial class Editar : ComponentBase
    {
        [Parameter]
        public string PrevistoId { get; set; }

        [Inject]
        private HttpClient _HttpClient { get; set; }
        [Inject]
        private NavigationManager _NavManager { get; set; }

        public Previsto Previsto { get; set; } = new Previsto();

        protected override async Task OnParametersSetAsync()
        {
            Previsto = await _HttpClient
                .GetFromJsonAsync<Previsto>($"Previstos/{PrevistoId}");
        }

        protected async Task HandleValidSubmitAsync()
        {
            var Response = await _HttpClient
                .PutAsJsonAsync<Previsto>("Previstos", Previsto);

            var Success = await Response.Content.ReadFromJsonAsync<bool>();

            if (Success)
            {
                _NavManager.NavigateTo("Previstos");
            }
        }
    }
}
