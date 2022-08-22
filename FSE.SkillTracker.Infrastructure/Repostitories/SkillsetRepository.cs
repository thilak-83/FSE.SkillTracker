using FSE.SkillTracker.Application.Intefaces;
using FSE.SkillTracker.Application.Interfaces;
using FSE.SkillTracker.Domain.Entities;
using FSE.SkillTracker.Infrastructure.Repostitories.Base;
using Microsoft.Azure.Cosmos;

namespace FSE.SkillTracker.Infrastructure.Repostitories
{
    public class SkillsetRepository : CosmosRepository<TechnicalSkills>, ISkillsetRepository
    {
        public override string ContainerName { get; } = "skillsets";
        public SkillsetRepository(ICosmosContainerFactory factory) : base(factory)
        {
            factory.EnsureDbSetupAsync ();
        }
        public override PartitionKey ResolvePartitionKey(TechnicalSkills entity)
        {
            return new PartitionKey(entity.Name);
        }
    }
}
