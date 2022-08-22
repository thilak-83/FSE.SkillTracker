using Newtonsoft.Json;

namespace FSE.SkillTracker.Domain
{
    public class EntityKey
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
    }
}
