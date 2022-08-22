using FSE.SkillTracker.Application.Intefaces;
using FSE.SkillTracker.Application.Interfaces;
using FSE.SkillTracker.Domain.Entities;
using FSE.SkillTracker.Infrastructure.Repostitories.Base;
using Microsoft.Azure.Cosmos;

namespace FSE.SkillTracker.Infrastructure.Repostitories
{
    public class ProfileRepository : CosmosRepository<Profile>, IProfileRepository
    {
        public override string ContainerName { get; } = "profiles";
        public ProfileRepository(ICosmosContainerFactory factory) : base(factory)
        {
            factory.EnsureDbSetupAsync ();
        }
        public override PartitionKey ResolvePartitionKey(Profile entity)
        {
            return new PartitionKey(entity.Name);
        }
    }
}
