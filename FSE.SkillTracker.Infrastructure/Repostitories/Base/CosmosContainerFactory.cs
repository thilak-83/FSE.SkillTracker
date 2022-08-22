using FSE.SkillTracker.Application.Intefaces;
using FSE.SkillTracker.Domain.Configurations;
using Microsoft.Azure.Cosmos;

namespace FSE.SkillTracker.Infrastructure.Repostitories.Base
{
    public class CosmosContainerFactory : ICosmosContainerFactory
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;
        private readonly List<ContainerInfo> _containers;

        public CosmosContainerFactory(string endpointUrl, string authKey, string databaseName, List<ContainerInfo> containers)
        {
            _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            _containers = containers ?? throw new ArgumentNullException(nameof(containers));

            CosmosClientOptions clientOptions = new CosmosClientOptions()
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            };
            _cosmosClient = new CosmosClient(endpointUrl, authKey);
        }

        public ContainerInfo GetContainerInfo(string containerName)
        {
            var container = _containers.FirstOrDefault(x => x.Name == containerName);
            if (container == null)
            {
                throw new ArgumentException($"Unable to find container: {containerName}");
            }
            return container;
        }

        public ICosmosContainer GetContainer(string containerName)
        {
            if (_containers.Where(x => x.Name == containerName) == null)
            {
                throw new ArgumentException($"Unable to find container: {containerName}");
            }
            return new CosmosContainer(_cosmosClient, _databaseName, containerName);
        }

        public void EnsureDbSetupAsync()
        {
            DatabaseResponse database = _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName).GetAwaiter().GetResult();

            foreach (ContainerInfo container in _containers.Where(c => c.EnsureExists))
            {
                database.Database.CreateContainerIfNotExistsAsync(container.Name, $"{container.PartitionKeyPath}").GetAwaiter().GetResult();
            }
        }
    }
}
