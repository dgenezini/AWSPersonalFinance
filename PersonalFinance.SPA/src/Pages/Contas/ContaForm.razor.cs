using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Pages.Contas
{
    public partial class ContaForm : ComponentBase
    {
        [Parameter] public Conta Conta { get; set; }
        [Parameter] public string BotaoDescricao { get; set; }
        [Parameter] public EventCallback OnValidSubmit { get; set; }
        [Parameter] public bool IsReadOnly { get; set; }
    }
}
