using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Pages.Categorias
{
    public partial class CategoriaForm : ComponentBase
    {
        [Parameter] public Categoria Categoria { get; set; }
        [Parameter] public string BotaoDescricao { get; set; }
        [Parameter] public EventCallback OnValidSubmit { get; set; }
        [Parameter] public bool IsReadOnly { get; set; }
    }
}
