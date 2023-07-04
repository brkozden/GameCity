namespace ETicaretMVC.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; }
    }
}
