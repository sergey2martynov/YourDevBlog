namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, Guid entityId)
            : base($"Entity \"{entityName}\" with ID \"{entityId}\" was not found.")
        {
            EntityName = entityName;
            EntityId = entityId;
        }

        public string EntityName { get; }
        public Guid EntityId { get; }
    }
}
