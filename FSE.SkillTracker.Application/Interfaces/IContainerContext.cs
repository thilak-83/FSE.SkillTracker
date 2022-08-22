using FSE.SkillTracker.Domain;
using Microsoft.Azure.Cosmos;

namespace FSE.SkillTracker.Application.Intefaces
{
    public interface IContainerContext<T> where T : EntityKey
    {
        string ContainerName { get; }
        PartitionKey ResolvePartitionKey(T entity);
    }
}
