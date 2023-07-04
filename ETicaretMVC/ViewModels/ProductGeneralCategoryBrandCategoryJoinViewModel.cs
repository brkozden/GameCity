namespace ETicaretMVC.ViewModels
{
    public class ProductGeneralCategoryBrandCategoryJoinViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImagePath { get; set; }
        public string? ImagePath2 { get; set; }
        public string? ImagePath3 { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string GeneralCategoryName { get; set; }
        public int GeneralCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }


    }
}
