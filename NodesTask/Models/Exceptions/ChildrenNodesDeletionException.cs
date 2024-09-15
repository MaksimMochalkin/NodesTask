namespace NodesTask.Models.Exceptions
{
    public class ChildrenNodesDeletionException : SecureException
    {
        public ChildrenNodesDeletionException(string message)
            : base(message)
        {
        }
    }
}
