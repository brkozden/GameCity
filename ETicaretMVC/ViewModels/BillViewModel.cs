namespace ETicaretMVC.ViewModels
{
    public class BillViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; }
        public string Address { get; set; }
    }
}
