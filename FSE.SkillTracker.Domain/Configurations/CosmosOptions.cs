namespace FSE.SkillTracker.Domain.Configurations
{
    public class CosmosOptions
    {
        public string EndpointUrl { get; set; }
        public string AuthKey { get; set; }
        public string DatabaseName { get; set; }
        public List<ContainerInfo> Containers { get; set; }
    }

    public class ContainerInfo
    {
        public string Name { get; set; }
        public string PartitionKeyPath { get; set; }
        public bool EnsureExists { get; set; } = false;
    }
}
