namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, object id)
            : base($"Entity \"{entityName}\" with ID \"{id}\" was not found.")
        {
            EntityName = entityName;
            EntityId = id;
        }

        public string EntityName { get; }
        public object EntityId { get; }
    }
}
