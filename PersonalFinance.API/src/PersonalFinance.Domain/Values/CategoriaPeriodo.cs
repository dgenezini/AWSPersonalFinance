using System;

namespace PersonalFinance.Domain.Values
{
    public class CategoriaPeriodo
    {
        public Guid? CategoriaId { get; set; }
        public DateTime Periodo { get; set; }
        public float Valor { get; set; }
    }
}
