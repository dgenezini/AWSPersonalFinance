using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Repositories
{
    public class ContaRepository : GenericRepository<Conta>
    {
        public ContaRepository(AmazonDynamoDBClient amazonDynamoDBClient) :
            base(amazonDynamoDBClient)
        {
        }

        public async Task<IEnumerable<Conta>> GetAllAsync()
        {
            IEnumerable<Conta> results = await ScanAsync(new ScanFilter());

            return results;
        }

        public async Task<Conta> GetByIdAsync(Guid key)
        {
            return await GetByKeyAsync(key.ToString());
        }

        public override async Task<bool> UpdateAsync(Conta entity)
        {
            var result = await base.UpdateAsync(entity);

            return result;
        }

        public override async Task<bool> AddAsync(Conta entity)
        {
            entity.ContaId = Guid.NewGuid();

            var result = await base.AddAsync(entity);

            return result;
        }

        public override async Task<bool> DeleteAsync(string key)
        {
            var result = await base.DeleteAsync(key);

            return result;
        }
    }
}
