using Microsoft.Azure.Cosmos;

namespace FSE.SkillTracker.Application.Intefaces
{
    public interface ICosmosContainer
    {
        Container _container { get; }
    }
}
