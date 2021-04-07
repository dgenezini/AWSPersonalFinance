using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PersonalFinance.Domain.Values
{
    public class ResumoMensal
    {
        public IEnumerable<Categoria> Categorias { get; set; }
        public List<DateTime> Periodos { get; set; }
        public List<Previsto> Previstos { get; set; }
        public IEnumerable<CategoriaPeriodo> CategoriasPeriodos { get; set; }
    }
}
