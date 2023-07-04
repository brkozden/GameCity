using Microsoft.Build.Framework;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;


namespace ETicaretMVC.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Genel Kategori alanı boş olamaz.")]
        public int GeneralCategoryId { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Kategori alanı boş olamaz.")]
        public int CategoryId { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Marka alanı boş olamaz.")]
        public int BrandId { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "İsim alanı boş olamaz.")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Açıklama alanı boş olamaz.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Açıklama alanı 10 ila 200 karakter arasında olmalıdır.")]
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Fiyat alanı boş olamaz.")]
        [Range(1, 1000000, ErrorMessage = "Fiyat alani 1 ila 1000000 arasinda olmalidir.")]
        public decimal Price { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Stok alanı boş olamaz.")]
        public int Stock { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Renk alanı boş olamaz.")]
        public string Color { get; set; }
        
        [ValidateNever]
        public IFormFile? Image { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }
        [ValidateNever]
        public IFormFile? Image2 { get; set; }

        [ValidateNever]
        public string ImagePath2 { get; set; }
        [ValidateNever]
        public IFormFile? Image3 { get; set; }

        [ValidateNever]
        public string ImagePath3 { get; set; }
    }
}
