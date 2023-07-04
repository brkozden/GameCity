namespace ETicaretMVC.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public string UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Situation { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

    }
}
