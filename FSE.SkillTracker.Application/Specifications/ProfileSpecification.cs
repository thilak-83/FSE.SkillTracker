
using FSE.SkillTracker.Application.Intefaces;

namespace FSE.SkillTracker.Application.Specifications
{
    public class ProfileSpecification : SpecificationBase, ICosmosQuerySpecification<Domain.Entities.Profile>
    {
        public ProfileSpecification()
        {
            OrderByClause = "order by c.id";
        }
    }
}
