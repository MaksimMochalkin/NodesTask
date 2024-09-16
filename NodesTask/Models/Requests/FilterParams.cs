namespace NodesTask.Models.Requests
{
    public class FilterParams
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string? Search { get; set; }
    }
}
