namespace FSE.SkillTracker.Domain.Entities
{
    public class Profile : EntityKey
    {
        public Profile()
        {
            SkillExpertise = new List<SkillExpertise> { };
        }
        public string Name { get; set; }
        public string AssociateId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public List<SkillExpertise> SkillExpertise { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class SkillExpertise
    {
        //public Guid SkillId { get; set; }
        public string? SkillName { get; set; }
        public int Expertise { get; set; }
    }
}
