using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PersonalFinance.Pages.Transacoes
{
    public partial class TransacaoForm : ComponentBase
    {
        [Parameter] public Transacao Transacao { get; set; }
        [Parameter] public string BotaoDescricao { get; set; }
        [Parameter] public EventCallback OnValidSubmit { get; set; }
        [Parameter] public bool IsReadOnly { get; set; }

        [Inject]
        private HttpClient _HttpClient { get; set; }

        public IEnumerable<Conta> ListContas { get; set; } = new List<Conta>();
        public IEnumerable<Categoria> ListCategorias { get; set; } = new List<Categoria>();

        protected override async Task OnInitializedAsync()
        {
            ListContas = await _HttpClient
                .GetFromJsonAsync<IEnumerable<Conta>>("Contas");
            ListCategorias = await _HttpClient
                .GetFromJsonAsync<IEnumerable<Categoria>>("Categorias");
        }
    }
}
