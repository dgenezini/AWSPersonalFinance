using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PersonalFinance.Pages.FormasPagamento
{
    [Authorize]
    public partial class Index : ComponentBase
    {
        [Inject]
        private HttpClient _HttpClient { get; set; }

        private IEnumerable<FormaPagamento> FormasPagamento { get; set; }

        protected override async Task OnInitializedAsync()
        {
            FormasPagamento = await _HttpClient
                .GetFromJsonAsync<IEnumerable<FormaPagamento>>("FormasPagamento");
        }
    }
}
