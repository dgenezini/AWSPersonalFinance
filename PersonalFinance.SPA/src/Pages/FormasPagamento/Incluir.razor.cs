using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PersonalFinance.Pages.FormasPagamento
{
    [Authorize]
    public partial class Incluir : ComponentBase
    {
        [Inject]
        private HttpClient _HttpClient { get; set; }
        [Inject]
        private NavigationManager _NavManager { get; set; }

        public FormaPagamento FormaPagamento { get; set; }

        protected override void OnInitialized()
        {
            FormaPagamento = new FormaPagamento();
        }

        protected async Task HandleValidSubmitAsync()
        {
            var Response = await _HttpClient
                .PostAsJsonAsync<FormaPagamento>("FormasPagamento", FormaPagamento);

            var Success = await Response.Content.ReadFromJsonAsync<bool>();

            if (Success)
            {
                _NavManager.NavigateTo("FormasPagamento");
            }
        }
    }
}
