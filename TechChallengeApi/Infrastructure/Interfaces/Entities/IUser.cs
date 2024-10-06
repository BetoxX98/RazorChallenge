namespace Infrastructure.Interfaces.Entities
{
    public interface IUser
    {
        string Id { get; }
        string Name { get; }
        public bool IsAuthenticated();
    }
}
