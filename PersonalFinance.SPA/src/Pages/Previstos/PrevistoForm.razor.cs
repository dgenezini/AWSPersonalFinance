using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PersonalFinance.Pages.Previstos
{
    public partial class PrevistoForm : ComponentBase
    {
        [Parameter] public Previsto Previsto { get; set; }
        [Parameter] public string BotaoDescricao { get; set; }
        [Parameter] public EventCallback OnValidSubmit { get; set; }
        [Parameter] public bool IsReadOnly { get; set; }

        [Inject]
        private HttpClient _HttpClient { get; set; }

        public IEnumerable<Categoria> ListCategorias { get; set; } = new List<Categoria>();

        protected override async Task OnInitializedAsync()
        {
            ListCategorias = await _HttpClient
                .GetFromJsonAsync<IEnumerable<Categoria>>("Categorias");
        }
    }
}
