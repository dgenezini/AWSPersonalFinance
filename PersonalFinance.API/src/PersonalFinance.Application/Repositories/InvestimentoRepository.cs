using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Repositories
{
    public class InvestimentoRepository : GenericRepository<Investimento>
    {
        public InvestimentoRepository(AmazonDynamoDBClient amazonDynamoDBClient) :
            base(amazonDynamoDBClient)
        {
        }

        public async Task<IEnumerable<Investimento>> GetAllAsync()
        {
            var scanFilter = new ScanFilter();

            return await ScanAsync(scanFilter);
        }

        public async Task<Investimento> GetByIdAsync(Guid key)
        {
            return (await QueryAsync(key.ToString())).SingleOrDefault();
        }

        public override async Task<bool> AddAsync(Investimento entity)
        {
            entity.InvestimentoId = Guid.NewGuid();
            entity.DataAtualizacao = DateTime.Now;

            var result = await base.AddAsync(entity);

            return result;
        }
    }
}
