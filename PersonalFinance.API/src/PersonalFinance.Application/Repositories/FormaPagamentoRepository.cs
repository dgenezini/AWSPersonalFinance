using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Repositories
{
    public class FormaPagamentoRepository : GenericRepository<FormaPagamento>
    {
        public FormaPagamentoRepository(AmazonDynamoDBClient amazonDynamoDBClient) :
            base(amazonDynamoDBClient)
        {
        }

        public async Task<IEnumerable<FormaPagamento>> GetAllAsync()
        {
            IEnumerable<FormaPagamento> results = await ScanAsync(new ScanFilter());

            return results;
        }

        public async Task<FormaPagamento> GetByIdAsync(Guid key)
        {
            return (await QueryAsync(key.ToString())).SingleOrDefault();
        }

        public override async Task<bool> UpdateAsync(FormaPagamento entity)
        {
            var result = await base.UpdateAsync(entity);

            return result;
        }

        public override async Task<bool> AddAsync(FormaPagamento entity)
        {
            entity.FormaPagamentoId = Guid.NewGuid();

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
