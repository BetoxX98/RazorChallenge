namespace Resources.Dtos.Product
{
    public class ProductPutDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public Guid? TypeId { get; set; }
    }
}
