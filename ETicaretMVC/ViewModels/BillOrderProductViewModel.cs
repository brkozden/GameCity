namespace ETicaretMVC.ViewModels
{
    public class BillOrderProductViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ImagePath { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
