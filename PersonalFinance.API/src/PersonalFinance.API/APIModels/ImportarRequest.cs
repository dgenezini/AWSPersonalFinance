using PersonalFinance.Domain.Values;
using System.IO;

namespace PersonalFinance.API.APIModels
{
    public class ImportarRequest
    {
        public ImportacaoTipo ImportacaoTipo { get; set; }
        public Stream Arquivo { get; set; }
    }
}
