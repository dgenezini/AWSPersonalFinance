using System;

namespace PersonalFinance.Domain.Entities
{
    public class Previsto
    {
        public Guid PrevistoId { get; set; }
        public int? Mes { get; set; }
        public int? Ano { get; set; }
        public Guid? CategoriaId { get; set; }
        public float Valor { get; set; }

        public string PK
        {
            get
            {
                return $"{TipoEntidade}#{PrevistoId}";
            }
        }

        public string SK
        {
            get
            {
                return $"{Ano:0000}-{Mes:00}";
            }
        }

        public string TipoEntidade
        {
            get
            {
                return $"Previsto";
            }
        }
    }
}
