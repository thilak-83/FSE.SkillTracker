using FSE.SkillTracker.Application.Intefaces;
using Microsoft.Azure.Cosmos;

namespace FSE.SkillTracker.Infrastructure.Repostitories.Base
{
    public class CosmosContainer : ICosmosContainer
    {
        public Container _container { get; }

        public CosmosContainer(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }
    }
}