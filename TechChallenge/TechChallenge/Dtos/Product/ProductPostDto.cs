namespace TechChallenge.Dtos.Product
{
    public class ProductPostDto
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public Guid? TypeId { get; set; }
    }
}
