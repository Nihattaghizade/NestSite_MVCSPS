namespace NestShop.Models
{
    public class Weight
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<ProductWeight> ProductWeight { get; set; }
        public Weight()
        {
            ProductWeight = new HashSet<ProductWeight>();
        }
    }
}
