namespace ETicaretMVC.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ProductName { get; set; }
        public string ProductImagePath { get; set; }
        public int ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
    }
}
