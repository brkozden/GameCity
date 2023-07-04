using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ETicaretMVC.ViewModels
{
    public class SearchViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int Stock { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }
        [ValidateNever]
        public IFormFile? Image2 { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }
        [ValidateNever]
        public string ImagePath2 { get; set; }
        public int? GeneralCategoryId { get; set; }

    }
}
