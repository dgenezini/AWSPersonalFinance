using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PersonalFinance.Pages.Categorias
{
    [Authorize]
    public partial class Excluir : ComponentBase
    {
        [Parameter]
        public string CategoriaId { get; set; }

        [Inject]
        private HttpClient _HttpClient { get; set; }
        [Inject]
        private NavigationManager _NavManager { get; set; }

        public Categoria Categoria { get; set; } = new Categoria();

        protected override async Task OnParametersSetAsync()
        {
            Categoria = await _HttpClient
                .GetFromJsonAsync<Categoria>($"Categorias/{CategoriaId}");
        }

        protected async Task HandleValidSubmitAsync()
        {
            var Response = await _HttpClient
                .DeleteAsync($"Categorias/{CategoriaId}");

            var Success = await Response.Content.ReadFromJsonAsync<bool>();

            if (Success)
            {
                _NavManager.NavigateTo("Categorias");
            }
        }
    }
}
