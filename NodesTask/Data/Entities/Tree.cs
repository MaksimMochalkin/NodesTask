namespace NodesTask.Data.Entities
{
    public class Tree
    {
        public Guid Id { get; set; }

        public string TreeName { get; set; } = string.Empty;

        public ICollection<Node> Nodes { get; set; } = new List<Node>();
    }
}
