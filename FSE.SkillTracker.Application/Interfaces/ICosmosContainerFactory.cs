using FSE.SkillTracker.Domain.Configurations;

namespace FSE.SkillTracker.Application.Intefaces
{
    public interface ICosmosContainerFactory
    {
        ICosmosContainer GetContainer(string containerName);
        ContainerInfo GetContainerInfo(string containerName);
        void EnsureDbSetupAsync();
    }
}
