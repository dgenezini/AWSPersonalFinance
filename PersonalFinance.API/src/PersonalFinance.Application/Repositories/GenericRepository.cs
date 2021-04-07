using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly AmazonDynamoDBClient _AmazonDynamoDBClient;
        protected readonly Table _Table;

        public GenericRepository(AmazonDynamoDBClient amazonDynamoDBClient) :
            this(amazonDynamoDBClient, typeof(T).Name)
        {
        }

        public GenericRepository(AmazonDynamoDBClient amazonDynamoDBClient,
            string tableName)
        {
            _AmazonDynamoDBClient = amazonDynamoDBClient;

            _Table = Table.LoadTable(_AmazonDynamoDBClient, tableName);
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            return await AddAsync(entity, new PutItemOperationConfig()
            {
                ReturnValues = ReturnValues.AllOldAttributes
            });
        }

        public virtual async Task<bool> AddAsync(T entity,
            PutItemOperationConfig putItemOperationConfig)
        {
            var jsonText = JsonSerializer.Serialize(entity);
            var item = Document.FromJson(jsonText);

            var result = await _Table.PutItemAsync(item, putItemOperationConfig);

            return result != null;
        }

        public virtual async Task AddMultipleAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                var jsonText = JsonSerializer.Serialize(entity);
                var item = Document.FromJson(jsonText);

                var result = await _Table.PutItemAsync(item);
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            var jsonText = JsonSerializer.Serialize(entity);
            var item = Document.FromJson(jsonText);

            var result = await _Table.UpdateItemAsync(item, new UpdateItemOperationConfig
            {
                ReturnValues = ReturnValues.AllNewAttributes
            });

            return result != null;
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            var jsonText = JsonSerializer.Serialize(entity);
            var item = Document.FromJson(jsonText);

            var result = await _Table.DeleteItemAsync(item, new DeleteItemOperationConfig()
            {
                ReturnValues = ReturnValues.AllOldAttributes
            });

            return result != null;
        }

        public virtual async Task<bool> DeleteAsync(string key)
        {
            var result = await _Table.DeleteItemAsync(key, new DeleteItemOperationConfig()
            {
                ReturnValues = ReturnValues.AllOldAttributes
            });

            return result != null;
        }

        public virtual async Task<bool> DeleteAsync(string key, string sortKey)
        {
            var result = await _Table.DeleteItemAsync(key, sortKey,
                new DeleteItemOperationConfig()
                {
                    ReturnValues = ReturnValues.AllOldAttributes
                });

            return result != null;
        }

        public async Task<T> GetByKeyAsync(string key, string sortKey)
        {
            var item = await _Table.GetItemAsync(key, sortKey);
            var jsonText = item.ToJson();

            return JsonSerializer.Deserialize<T>(jsonText);
        }

        public async Task<T> GetByKeyAsync(string key)
        {
            var item = await _Table.GetItemAsync(key);

            if (item == null)
            {
                return null;
            }

            var jsonText = item.ToJson();

            return JsonSerializer.Deserialize<T>(jsonText);
        }

        protected async Task<IEnumerable<T>> ScanAsync(ScanFilter scanFilter)
        {
            var search = _Table.Scan(scanFilter);

            return await GetResultsAsync(search);
        }

        protected async Task<IEnumerable<T>> ScanAsync(ScanOperationConfig scanOperationConfig)
        {
            var search = _Table.Scan(scanOperationConfig);

            return await GetResultsAsync(search);
        }

        protected async Task<IEnumerable<T>> QueryAsync(string pk)
        {
            var pkFilter = new QueryFilter("PK", QueryOperator.Equal,
                pk);

            var search = _Table.Query(pkFilter);

            return await GetResultsAsync(search);
        }

        protected async Task<IEnumerable<T>> QueryAsync(string pk, QueryFilter skFilter)
        {
            var search = _Table.Query(pk, skFilter);

            return await GetResultsAsync(search);
        }

        protected async Task<IEnumerable<T>> QueryAsync(QueryFilter queryFilter)
        {
            var search = _Table.Query(queryFilter);

            return await GetResultsAsync(search);
        }

        protected async Task<IEnumerable<T>> QueryAsync(QueryOperationConfig queryOperationConfig)
        {
            var search = _Table.Query(queryOperationConfig);

            return await GetResultsAsync(search);
        }

        protected async Task<IEnumerable<T>> QueryManyAsync(params string[] keys)
        {
            var BatchGet = _Table.CreateBatchGet();

            foreach (var key in keys)
            {
                BatchGet.AddKey(key);
            }

            await BatchGet.ExecuteAsync();

            return GetResults(BatchGet.Results);
        }

        private static async Task<IEnumerable<T>> GetResultsAsync(Search search)
        {
            var results = new List<T>();

            do
            {
                var documentList = await search.GetNextSetAsync();

                results.AddRange(GetResults(documentList));
            } while (!search.IsDone);

            return results;
        }

        private static IEnumerable<T> GetResults(IList<Document> documents)
        {
            var results = new List<T>();

            foreach (var document in documents)
            {
                var jsonText = document.ToJson();

                results.Add(JsonSerializer.Deserialize<T>(jsonText));
            }

            return results;
        }
    }
}
