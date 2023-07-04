using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ETicaretMVC.ViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal ProductPrice { get; set; }

        [ValidateNever]
        public string ProductImagePath { get; set; }
    }
}
