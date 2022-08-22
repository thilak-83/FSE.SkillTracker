using Microsoft.Azure.Cosmos;

namespace FSE.SkillTracker.Application.Intefaces
{
    public interface IContainer
    {
        Container _container { get; }
    }
}