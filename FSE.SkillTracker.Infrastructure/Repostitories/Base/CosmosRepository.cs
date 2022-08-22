using FSE.SkillTracker.Application.Intefaces;
using FSE.SkillTracker.Application.Interfaces;
using FSE.SkillTracker.Domain;
using Microsoft.Azure.Cosmos;

namespace FSE.SkillTracker.Infrastructure.Repostitories.Base
{
    public abstract class CosmosRepository<T> : IRepository<T>, IContainerContext<T> where T : EntityKey
    {
        private readonly ICosmosContainerFactory _cosmosContainerFactory;
        protected readonly Container _container;

        public virtual string ContainerName { get { return typeof(T).Name; } }

        /// <summary>
        /// Resolves the VALUE of the partition key (not the PATH)
        /// </summary>
        public abstract PartitionKey ResolvePartitionKey(T entity);

        public CosmosRepository(ICosmosContainerFactory cosmosContainerFactory)
        {
            _cosmosContainerFactory = cosmosContainerFactory ?? throw new ArgumentNullException(nameof(ICosmosContainerFactory));
            _container = _cosmosContainerFactory.GetContainer(ContainerName)._container;
        }

        public async Task AddItemAsync(T item)
        {
            var properties = await _container.ReadContainerAsync();
            await _container.CreateItemAsync<T>(item, ResolvePartitionKey(item));
        }

        public async Task DeleteItemAsync(string id, string partitionKeyValue)
        {
            await _container.DeleteItemAsync<T>(id.ToString(), new PartitionKey(partitionKeyValue));
        }

        public async Task<T> GetItemAsync(string id, string partitionKeyValue)
        {
            try
            {
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id.ToString(), new PartitionKey(partitionKeyValue));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        [Obsolete("Prefer using specification rather than querystring", false)]
       public async Task<IEnumerable<T>> GetItemsAsync(string sqlQuery)
        {
            var query = _container.GetItemQueryIterator<T>(new QueryDefinition(sqlQuery));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async IAsyncEnumerable<T> GetItemsAsyncEnumerable(ICosmosQuerySpecification<T> specification)
        {
            var queryDefinition = specification.GetQueryDefinition();

            using var iterator = _container.GetItemQueryIterator<T>(queryDefinition);
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                foreach (var item in response)
                {
                    yield return item;
                }
            }
        }

        public virtual async Task<IEnumerable<T>> GetItemsAsync(ICosmosQuerySpecification<T> specification)
        {
            var queryDefinition = specification.GetQueryDefinition();
            var iterator = _container.GetItemQueryIterator<T>(queryDefinition);

            List<T> results = new List<T>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();

                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateItemAsync(T item)
        {
            await _container.UpsertItemAsync<T>(item);//, ResolvePartitionKey()); // partition key throws key not matching exception
        }
    }
}
