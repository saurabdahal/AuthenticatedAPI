namespace AuthentationWebAPI.Data
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Add a Category property
        public Category ProductCategory { get; set; }
    }
}
