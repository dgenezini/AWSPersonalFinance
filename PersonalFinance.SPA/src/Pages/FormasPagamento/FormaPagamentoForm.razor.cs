using Microsoft.AspNetCore.Components;
using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Pages.FormasPagamento
{
    public partial class FormaPagamentoForm : ComponentBase
    {
        [Parameter] public FormaPagamento FormaPagamento { get; set; }
        [Parameter] public string BotaoDescricao { get; set; }
        [Parameter] public EventCallback OnValidSubmit { get; set; }
        [Parameter] public bool IsReadOnly { get; set; }
    }
}
