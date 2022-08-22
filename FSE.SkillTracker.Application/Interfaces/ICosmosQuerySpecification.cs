using Microsoft.Azure.Cosmos;

namespace FSE.SkillTracker.Application.Intefaces
{
    public interface ICosmosQuerySpecification<T>
    {
        QueryDefinition GetQueryDefinition();
    }
}
