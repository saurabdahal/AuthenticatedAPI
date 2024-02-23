namespace AuthentationWebAPI.Data
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public required string User { get; set; }
        public required List<Product> Products { get; set; }
    }
}
