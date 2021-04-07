using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PersonalFinance.Pages.FormasPagamento
{
    [Authorize]
    public partial class Editar : ComponentBase
    {
        [Parameter]
        public string FormaPagamentoId { get; set; }

        [Inject]
        private HttpClient _HttpClient { get; set; }
        [Inject]
        private NavigationManager _NavManager { get; set; }

        public FormaPagamento FormaPagamento { get; set; } = new FormaPagamento();

        protected override async Task OnParametersSetAsync()
        {
            FormaPagamento = await _HttpClient
                .GetFromJsonAsync<FormaPagamento>($"FormasPagamento/{FormaPagamentoId}");
        }

        protected async Task HandleValidSubmitAsync()
        {
            var Response = await _HttpClient
                .PutAsJsonAsync<FormaPagamento>("FormasPagamento", FormaPagamento);

            var Success = await Response.Content.ReadFromJsonAsync<bool>();

            if (Success)
            {
                _NavManager.NavigateTo("FormasPagamento");
            }
        }
    }
}
