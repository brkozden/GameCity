namespace ETicaretMVC.ViewModels
{
    public class OrderUserViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Situation { get; set; }
        public int BillId { get; set; }
    }
}
