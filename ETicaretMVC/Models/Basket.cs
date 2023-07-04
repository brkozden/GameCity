namespace ETicaretMVC.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ProductName { get; set; }
        public string? ProductImagePath { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
