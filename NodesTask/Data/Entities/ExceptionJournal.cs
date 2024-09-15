namespace NodesTask.Data.Entities
{
    public class ExceptionJournal
    {
        public Guid EventId { get; set; }

        public DateTime Timestamp { get; set; }

        public string QueryParams { get; set; }

        public string BodyParams { get; set; }

        public string StackTrace { get; set; }

        public string ExceptionType { get; set; }
    }
}
