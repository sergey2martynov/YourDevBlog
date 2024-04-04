namespace Core.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }

        protected EntityBase() 
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }        
    }
}
