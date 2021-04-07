using System;

namespace PersonalFinance.Domain.Entities
{
    public class TransacaoLocal
    {
        public string LocalPagto { get; set; }
        public string Descricao { get; set; }
        public Guid? CategoriaId { get; set; }
        public DateTime DataHora { get; set; }

        public string PK
        {
            get
            {
                return LocalPagto;
            }
        }
    }
}
