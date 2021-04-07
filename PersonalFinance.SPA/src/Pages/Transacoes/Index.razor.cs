using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.CustomExtensions;
using PersonalFinance.Domain.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace PersonalFinance.Pages.Transacoes
{
    [Authorize]
    public partial class Index : ComponentBase
    {
        [Inject]
        private HttpClient _HttpClient { get; set; }
        [Inject]
        NavigationManager _NavigationManager { get; set; }
        
        private IEnumerable<Transacao> Transacoes { get; set; }
        private string ReturnUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ReturnUrl = HttpUtility.UrlEncode(_NavigationManager.ToBaseRelativePath(_NavigationManager.Uri));

            var QueryString = _NavigationManager.QueryString();

            var CategoriaId = QueryString["CategoriaId"];
            var Periodo = QueryString["Periodo"];
            var IsSemCategoria = QueryString["IsSemCategoria"];

            Transacoes = await _HttpClient
                .GetFromJsonAsync<IEnumerable<Transacao>>(
                    $"Transacoes?categoriaId={CategoriaId}&periodo={Periodo}&isSemCategoria={IsSemCategoria}");
        }
    }
}
