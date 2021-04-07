using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PersonalFinance.Pages.Contas
{
    [Authorize]
    public partial class Incluir : ComponentBase
    {
        [Inject]
        private HttpClient _HttpClient { get; set; }
        [Inject]
        private NavigationManager _NavManager { get; set; }

        public Conta Conta { get; set; }

        protected override void OnInitialized()
        {
            Conta = new Conta();
        }

        protected async Task HandleValidSubmitAsync()
        {
            var Response = await _HttpClient
                .PostAsJsonAsync<Conta>("Contas", Conta);

            var Success = await Response.Content.ReadFromJsonAsync<bool>();

            if (Success)
            {
                _NavManager.NavigateTo("Contas");
            }
        }
    }
}
