using Amazon.DynamoDBv2;
using PersonalFinance.Domain.Entities;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Repositories
{
    public class TransacaoLocalRepository : GenericRepository<TransacaoLocal>
    {
        public TransacaoLocalRepository(AmazonDynamoDBClient amazonDynamoDBClient) :
            base(amazonDynamoDBClient)
        {
        }

        public async Task<TransacaoLocal> GetByLocalAsync(string local)
        {
            return await GetByKeyAsync(local);
        }
    }
}
