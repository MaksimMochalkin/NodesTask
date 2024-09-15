namespace NodesTask.Data.Entities
{
    using Newtonsoft.Json;

    public class Node
    {
        public Guid Id { get; set; }

        public string NodeName { get; set; } = string.Empty;

        public Guid? ParentNodeId { get; set; }

        [JsonIgnore]
        public Node ParentNode { get; set; }

        public Guid TreeId { get; set; }

        [JsonIgnore]
        public Tree Tree { get; set; }

        public ICollection<Node> Children { get; set; } = new List<Node>();
    }
}
