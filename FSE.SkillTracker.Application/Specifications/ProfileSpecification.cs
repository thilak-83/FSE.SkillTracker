
using FSE.SkillTracker.Application.Features.Profile.Queries;
using FSE.SkillTracker.Application.Intefaces;

namespace FSE.SkillTracker.Application.Specifications
{
    public class ProfileSpecification : SpecificationBase, ICosmosQuerySpecification<Domain.Entities.Profile>
    {
        public ProfileSpecification()
        {
            OrderByClause = "order by c.id";

        }

        public ProfileSpecification(GetProfilesByCriteriaQuery request)
        {
            OrderByClause = "order by c.id";
            if (request != null && request.Criteria != "Skill")
                AddCondition($"c.{request.Criteria} like '%{request.CriteriaValue}%'");
            else if (request != null && request.Criteria == "Skill")
            {
                OrderByClause = "";
                SelectClause = "Select p.Name,p.AssociateId,p.Mobile,p.Email, ARRAY(select s.SkillName, s.Expertise) as SkillExpertise";
                FromClause = "From Profiles p JOIN s IN p.SkillExpertise";
                AddCondition($"s.SkillName like '%{request.CriteriaValue}%' AND s.Expertise > 10");
            }

        }


    }
}
