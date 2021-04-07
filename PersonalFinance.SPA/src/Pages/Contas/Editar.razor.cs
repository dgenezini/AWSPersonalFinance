using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PersonalFinance.Pages.Contas
{
    [Authorize]
    public partial class Editar : ComponentBase
    {
        [Parameter]
        public string ContaId { get; set; }

        [Inject]
        private HttpClient _HttpClient { get; set; }
        [Inject]
        private NavigationManager _NavManager { get; set; }

        public Conta Conta { get; set; } = new Conta();

        protected override async Task OnParametersSetAsync()
        {
            Conta = await _HttpClient
                .GetFromJsonAsync<Conta>($"Contas/{ContaId}");
        }

        protected async Task HandleValidSubmitAsync()
        {
            var Response = await _HttpClient
                .PutAsJsonAsync<Conta>("Contas", Conta);

            var Success = await Response.Content.ReadFromJsonAsync<bool>();

            if (Success)
            {
                _NavManager.NavigateTo("Contas");
            }
        }
    }
}
