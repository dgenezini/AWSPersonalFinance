using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.CustomExtensions;
using PersonalFinance.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PersonalFinance.Pages.Transacoes
{
    [Authorize]
    public partial class Incluir : ComponentBase
    {
        [Inject]
        private HttpClient _HttpClient { get; set; }
        [Inject]
        private NavigationManager _NavigationManager { get; set; }

        public Transacao Transacao { get; set; }
        public string ReturnUrl { get; set; }

        protected override void OnInitialized()
        {
            Transacao = new Transacao();

            ReturnUrl = _NavigationManager.QueryString("ReturnUrl");
        }

        protected async Task HandleValidSubmitAsync()
        {
            var Response = await _HttpClient
                .PostAsJsonAsync<Transacao>("Transacoes", Transacao);

            var Success = await Response.Content.ReadFromJsonAsync<bool>();

            if (Success)
            {
                _NavigationManager.NavigateTo(ReturnUrl);
            }
        }
    }
}
