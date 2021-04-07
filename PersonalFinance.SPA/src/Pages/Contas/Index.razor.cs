using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace PersonalFinance.Pages.Contas
{
    [Authorize]
    public partial class Index : ComponentBase
    {
        [Inject]
        private HttpClient _HttpClient { get; set; }
        [Inject]
        NavigationManager _NavigationManager { get; set; }

        private IEnumerable<Conta> Contas { get; set; }
        private string ReturnUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ReturnUrl = HttpUtility.UrlEncode(_NavigationManager.ToBaseRelativePath(_NavigationManager.Uri));

            Contas = await _HttpClient
                .GetFromJsonAsync<IEnumerable<Conta>>("Contas");
        }
    }
}
