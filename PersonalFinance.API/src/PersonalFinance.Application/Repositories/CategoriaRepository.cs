using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Repositories
{
    public class CategoriaRepository : GenericRepository<Categoria>
    {
        public CategoriaRepository(AmazonDynamoDBClient amazonDynamoDBClient) :
            base(amazonDynamoDBClient)
        {
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            IEnumerable<Categoria> results = await ScanAsync(new ScanFilter());

            return results;
        }

        public async Task<Categoria> GetByIdAsync(Guid key)
        {
            return await GetByKeyAsync(key.ToString());
        }

        public override async Task<bool> UpdateAsync(Categoria entity)
        {
            var result = await base.UpdateAsync(entity);

            return result;
        }

        public override async Task<bool> AddAsync(Categoria entity)
        {
            entity.CategoriaId = Guid.NewGuid();

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
