using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Repositories
{
    public class PrevistoRepository : GenericRepository<Previsto>
    {
        public PrevistoRepository(AmazonDynamoDBClient amazonDynamoDBClient) :
            base(amazonDynamoDBClient, "Geral")
        {
        }

        public async Task<Previsto> GetByIdAsync(Guid previstoId)
        {
            return (await QueryAsync($"Previsto#{previstoId}")).SingleOrDefault();
        }

        public async Task<IEnumerable<Previsto>> GetByPeriodoAsync(int? ano, int? mes)
        {
            var QueryFilter = new QueryFilter("SK", QueryOperator.Equal, $"{ano:0000}-{mes:00}");
            QueryFilter.AddCondition("TipoEntidade", QueryOperator.Equal, "Previsto");

            var QueryOperationConfig = new QueryOperationConfig()
            {
                Filter = QueryFilter,
                IndexName = "InvertedIndex"
            };

            return await QueryAsync(QueryOperationConfig);
        }

        public override async Task<bool> UpdateAsync(Previsto entity)
        {
            var result = await base.UpdateAsync(entity);

            return result;
        }

        public override async Task<bool> AddAsync(Previsto entity)
        {
            entity.PrevistoId = Guid.NewGuid();

            var result = await base.AddAsync(entity);

            return result;
        }

        public override async Task<bool> DeleteAsync(Previsto entity)
        {
            var result = await base.DeleteAsync(entity);

            return result;
        }
    }
}
