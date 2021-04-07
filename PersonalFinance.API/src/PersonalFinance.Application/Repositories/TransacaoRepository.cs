using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Repositories
{
    public class TransacaoRepository : GenericRepository<Transacao>
    {
        public TransacaoRepository(AmazonDynamoDBClient amazonDynamoDBClient) :
            base(amazonDynamoDBClient, "Geral")
        {
        }

        public async Task<IEnumerable<Transacao>> GetByPeriodoAsync(int ano, int mes)
        {
            var QueryFilter = new QueryFilter("SK", QueryOperator.Equal, $"{ano:0000}-{mes:00}");
            QueryFilter.AddCondition("TipoEntidade", QueryOperator.Equal, "Transacao");

            var QueryOperationConfig = new QueryOperationConfig()
            {
                Filter = QueryFilter,
                IndexName = "InvertedIndex"
                //PaginationToken = lastKey,
                //Limit = 11
            };

            return await QueryAsync(QueryOperationConfig);
        }

        public async Task<Transacao> GetByIdAsync(Guid key)
        {
            return (await QueryAsync($"Transacao#{key.ToString()}")).SingleOrDefault();
        }

        public async Task<IEnumerable<Transacao>> GetByPeriodoAsync(
            DateTime dataInicial, DateTime dataFinal)
        {
            var ScanFilter = new ScanFilter();
            ScanFilter.AddCondition("SK", ScanOperator.Between,
                dataInicial.ToString("yyyy-MM-dd"), dataFinal.ToString("yyyy-MM-dd"));
            ScanFilter.AddCondition("TipoEntidade", ScanOperator.Equal, "Transacao");

            var ScanOperationConfig = new ScanOperationConfig()
            {
                IndexName = "InvertedIndex",
                Filter = ScanFilter
            };

            return await ScanAsync(ScanOperationConfig);
        }

        public override async Task<bool> AddAsync(Transacao entity)
        {
            entity.TransacaoId = Guid.NewGuid();

            if (string.IsNullOrEmpty(entity.Moeda))
            {
                entity.Moeda = "R$";
            }

            entity.BeforeUpdate();

            var result = await base.AddAsync(entity);

            return result;
        }

        public override async Task<bool> DeleteAsync(string transacaoId, string sortKey)
        {
            return await base.DeleteAsync($"Transacao#{transacaoId}", sortKey);
        }
    }
}
