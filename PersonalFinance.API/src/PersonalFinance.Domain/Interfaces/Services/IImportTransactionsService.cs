using PersonalFinance.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PersonalFinance.Domain.Interfaces.Services
{
    public interface IImportTransactionsService
    {
        string Description { get; }

        Task<IEnumerable<Transacao>> GetTransactionsAsync(Stream XLSStream);
    }
}
