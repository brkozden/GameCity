using AutoMapper;
using ETicaretMVC.Areas.Identity.Data;
using ETicaretMVC.Models;
using ETicaretMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretMVC.Controllers
{
    public class MainpageController : Controller
    {
        private AppDbContext _context;
        private readonly ILogger<MainpageController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public MainpageController(ILogger<MainpageController> logger, AppDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public int getBasketCount()
        {
            var userId = _userManager.GetUserId(User);
            var basketCount = _context.Baskets.Where(x => x.UserId == userId).Count();
            return basketCount;
        }
        public IActionResult Index()
        {
            ViewData["BasketCount"] = getBasketCount();
            return View();
        }

        public IActionResult Slider(int page, int brand, int category, string name,int sortBy)
        {
            ViewData["BasketCount"] = getBasketCount();

            if (sortBy != 0)
            {
                if (sortBy ==1)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == brand && x.CategoryId == category && x.Name.Contains(name)).OrderByDescending(x=> x.Id).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.Name = name;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else if (sortBy == 2)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        Stock = x.Stock

                    }).Where(x => x.BrandId == brand && x.CategoryId == category && x.Name.Contains(name)).OrderBy(x=> x.Stock).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.Name = name;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else if (sortBy == 3)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == brand && x.CategoryId == category && x.Name.Contains(name)).OrderBy(x=> x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.Name = name;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == brand && x.CategoryId == category && x.Name.Contains(name)).OrderByDescending(x=> x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.Name = name;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
            }
            else
            {
                var products = _context.Products.Select(x => new SearchViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    BrandId = x.BrandId,
                    ImagePath = x.ImagePath,
                    ImagePath2 = x.ImagePath2

                }).Where(x => x.BrandId == brand && x.CategoryId == category && x.Name.Contains(name)).ToList();

                var productCount = Convert.ToDouble(products.Count);
                double pageDouble = productCount / 9;
                double pageRounding = Math.Ceiling(pageDouble);
                int pageCount = Convert.ToInt32(pageRounding);

                ViewBag.PageCount = pageCount;
                ViewBag.Page = page;
                ViewBag.Brand = brand;
                ViewBag.Category = category;
                ViewBag.Name = name;

                var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                return View(productsPaged);
            }
            
        }

        public IActionResult GeneralCategory(int page,int generalCategory, int sortBy, int category)
        {
            ViewBag.Categories1 = new Dictionary<string, int>()
            {
                {"Dizüstü Bilgisayar", 1},
                {"Masaüstü Bilgisayar", 2},
                {"All-in-One Bilgisayar", 3}
            };
            ViewBag.Categories2 = new Dictionary<string, int>()
            {
                {"Anakart", 4},
                {"İşlemci", 5},
                {"Ekran Kartı", 6},
                {"Ram", 7},
                {"SSD", 8},
                {"Hard Disk", 9},
                {"Kasa", 10},
                {"Güç Kaynağı (PSU)", 11}
            };
            ViewBag.Categories3 = new Dictionary<string, int>()
            {
                {"Soğutucular", 12},
                {"Monitör", 13},
                {"Klavye", 14},
                {"Klavye - Mouse Set", 15},
                {"Kulaklık", 16},
                {"Mouse", 17},
                {"Mikrofon", 18},
                {"Web Camera", 19},
                {"Aksesuarlar", 20}
            };
            ViewBag.Categories4 = new Dictionary<string, int>()
            {
                {"Office Yazılımları", 21},
                {"İşletim Sistemleri", 22},
                {"Antivirüs ve Güvenlik", 23}
            };
            ViewData["BasketCount"] = getBasketCount();

            if (sortBy != 0 && category ==0)
            {
                if (sortBy ==1)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        GeneralCategoryId = x.GeneralCategoryId
                    }).Where(x => x.GeneralCategoryId == generalCategory).OrderByDescending(x=> x.Id).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.GeneralCategory = generalCategory;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else if (sortBy == 2)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        GeneralCategoryId = x.GeneralCategoryId,
                        Stock = x.Stock
                    }).Where(x => x.GeneralCategoryId == generalCategory).OrderBy(x=> x.Stock).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.GeneralCategory = generalCategory;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else if (sortBy == 3)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        GeneralCategoryId = x.GeneralCategoryId
                    }).Where(x => x.GeneralCategoryId == generalCategory).OrderBy(x=> x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.GeneralCategory = generalCategory;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        GeneralCategoryId = x.GeneralCategoryId
                    }).Where(x => x.GeneralCategoryId == generalCategory).OrderByDescending(x=> x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.GeneralCategory = generalCategory;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
            }
            else if (sortBy == 0 && category != 0)
            {
                var products = _context.Products.Select(x => new SearchViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    ImagePath = x.ImagePath,
                    ImagePath2 = x.ImagePath2,
                    GeneralCategoryId = x.GeneralCategoryId
                }).Where(x => x.CategoryId == category).ToList();

                var productCount = Convert.ToDouble(products.Count);
                double pageDouble = productCount / 9;
                double pageRounding = Math.Ceiling(pageDouble);
                int pageCount = Convert.ToInt32(pageRounding);

                ViewBag.Category = category;
                ViewBag.PageCount = pageCount;
                ViewBag.Page = page;
                ViewBag.GeneralCategory = generalCategory;

                var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                return View(productsPaged);
            }
            else if (sortBy != 0 && category !=0)
            {
                if (sortBy == 1)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        GeneralCategoryId = x.GeneralCategoryId
                    }).Where(x => x.CategoryId ==category).OrderByDescending(x => x.Id).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.GeneralCategory = generalCategory;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else if (sortBy == 2)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        GeneralCategoryId = x.GeneralCategoryId,
                        Stock = x.Stock
                    }).Where(x => x.CategoryId == category).OrderBy(x => x.Stock).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.GeneralCategory = generalCategory;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else if (sortBy == 3)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        GeneralCategoryId = x.GeneralCategoryId
                    }).Where(x => x.CategoryId == category).OrderBy(x => x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.GeneralCategory = generalCategory;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        GeneralCategoryId = x.GeneralCategoryId
                    }).Where(x => x.CategoryId == category).OrderByDescending(x => x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.GeneralCategory = generalCategory;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
            }
            else
            {
                var products = _context.Products.Select(x => new SearchViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    ImagePath = x.ImagePath,
                    ImagePath2 = x.ImagePath2,
                    GeneralCategoryId = x.GeneralCategoryId
                }).Where(x => x.GeneralCategoryId == generalCategory).ToList();

                var productCount = Convert.ToDouble(products.Count);
                double pageDouble = productCount / 9;
                double pageRounding = Math.Ceiling(pageDouble);
                int pageCount = Convert.ToInt32(pageRounding);

                ViewBag.PageCount = pageCount;
                ViewBag.Page = page;
                ViewBag.GeneralCategory = generalCategory;

                var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                return View(productsPaged);
            }

        }

        public IActionResult CampaignSingle(int page, int sortBy)
        {
            ViewData["BasketCount"] = getBasketCount();

            if (sortBy == 0)
            {
                var products = _context.Products.Select(x => new SearchViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    BrandId = x.BrandId,
                    ImagePath = x.ImagePath,
                    ImagePath2 = x.ImagePath2

                }).Where(x => x.BrandId == 8 && x.CategoryId == 5 && x.Name.Contains("13")).ToList();

                var productCount = Convert.ToDouble(products.Count);
                double pageDouble = productCount / 9;
                double pageRounding = Math.Ceiling(pageDouble);
                int pageCount = Convert.ToInt32(pageRounding);

                ViewBag.PageCount = pageCount;
                ViewBag.Page = page;

                var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                return View(productsPaged);
            }
            else
            {
                if (sortBy==1)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == 8 && x.CategoryId == 5 && x.Name.Contains("13")).OrderByDescending(x=> x.Id).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.SortBy = sortBy;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else if (sortBy == 2)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        Stock = x.Stock

                    }).Where(x => x.BrandId == 8 && x.CategoryId == 5 && x.Name.Contains("13")).OrderBy(x=> x.Stock).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.SortBy = sortBy;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else if (sortBy == 3)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == 8 && x.CategoryId == 5 && x.Name.Contains("13")).OrderBy(x=> x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.SortBy = sortBy;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == 8 && x.CategoryId == 5 && x.Name.Contains("13")).OrderByDescending(x=> x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.SortBy = sortBy;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
            }
        }

        public IActionResult Brands(int brand, int page,int category, int sortBy)
        {
            ViewBag.Categories1 = new Dictionary<string, int>()
            {
                {"Dizüstü Bilgisayar", 1},
                {"Masaüstü Bilgisayar", 2},
                {"All-in-One Bilgisayar", 3}
            };
            ViewBag.Categories2 = new Dictionary<string, int>()
            {
                {"Anakart", 4},
                {"İşlemci", 5},
                {"Ekran Kartı", 6},
                {"Ram", 7},
                {"SSD", 8},
                {"Hard Disk", 9},
                {"Kasa", 10},
                {"Güç Kaynağı (PSU)", 11}
            };
            ViewBag.Categories3 = new Dictionary<string, int>()
            {
                {"Soğutucular", 12},
                {"Monitör", 13},
                {"Klavye", 14},
                {"Klavye - Mouse Set", 15},
                {"Kulaklık", 16},
                {"Mouse", 17},
                {"Mikrofon", 18},
                {"Web Camera", 19},
                {"Aksesuarlar", 20}
            };
            ViewBag.Categories4 = new Dictionary<string, int>()
            {
                {"Office Yazılımları", 21},
                {"İşletim Sistemleri", 22},
                {"Antivirüs ve Güvenlik", 23}
            };
            ViewData["BasketCount"] = getBasketCount();

            if (category != 0 && sortBy == 0)
            {
                var products = _context.Products.Select(x => new SearchViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    BrandId = x.BrandId,
                    ImagePath = x.ImagePath,
                    ImagePath2 = x.ImagePath2

                }).Where(x => x.BrandId == brand && x.CategoryId == category).ToList();
                var productCount = Convert.ToDouble(products.Count);
                double pageDouble = productCount / 9;
                double pageRounding = Math.Ceiling(pageDouble);
                int pageCount = Convert.ToInt32(pageRounding);
                ViewBag.PageCount = pageCount;
                var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();
                ViewBag.Page = page;
                ViewBag.Brand = brand;
                ViewBag.Category = category;
                return View(productsPaged);
            }
            else if (category != 0 && sortBy != 0)
            {
                if (sortBy == 1)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == brand && x.CategoryId == category).OrderByDescending(x => x.Id).ToList();
                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);
                    ViewBag.PageCount = pageCount;
                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    return View(productsPaged);
                }
                else if (sortBy == 2)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        Stock = x.Stock

                    }).Where(x => x.BrandId == brand && x.CategoryId == category).OrderBy(x => x.Stock).ToList();
                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);
                    ViewBag.PageCount = pageCount;
                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;
                    return View(productsPaged);
                }
                else if (sortBy == 3)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == brand && x.CategoryId == category).OrderBy(x => x.Price).ToList();
                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);
                    ViewBag.PageCount = pageCount;
                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;
                    return View(productsPaged);
                }
                else
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == brand && x.CategoryId == category).OrderByDescending(x => x.Price).ToList();
                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);
                    ViewBag.PageCount = pageCount;
                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;
                    return View(productsPaged);
                }
            }
            else if (category == 0 && sortBy != 0)
            {
                if (sortBy == 1)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == brand).OrderByDescending(x => x.Id).ToList();
                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);
                    ViewBag.PageCount = pageCount;
                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    return View(productsPaged);
                }
                else if (sortBy == 2)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        Stock = x.Stock

                    }).Where(x => x.BrandId == brand).OrderBy(x => x.Stock).ToList();
                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);
                    ViewBag.PageCount = pageCount;
                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;
                    return View(productsPaged);
                }
                else if (sortBy == 3)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == brand).OrderBy(x => x.Price).ToList();
                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);
                    ViewBag.PageCount = pageCount;
                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;
                    return View(productsPaged);
                }
                else
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == brand).OrderByDescending(x => x.Price).ToList();
                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);
                    ViewBag.PageCount = pageCount;
                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;
                    return View(productsPaged);
                }
            }
            else
            {
                var products = _context.Products.Select(x => new SearchViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    BrandId = x.BrandId,
                    ImagePath = x.ImagePath,
                    ImagePath2 = x.ImagePath2

                }).Where(x => x.BrandId == brand).ToList();
                var productCount = Convert.ToDouble(products.Count);
                double pageDouble = productCount / 9;
                double pageRounding = Math.Ceiling(pageDouble);
                int pageCount = Convert.ToInt32(pageRounding);
                ViewBag.PageCount = pageCount;
                var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();
                ViewBag.Page = page;
                ViewBag.Brand = brand;
                ViewBag.Category = category;
                return View(productsPaged);
            }
        }

        public IActionResult Campaigns(int page, int brand, int category, int sortBy)
        {
            ViewData["BasketCount"] = getBasketCount();

            if (sortBy ==0)
            {
                var products = _context.Products.Select(x => new SearchViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    BrandId = x.BrandId,
                    ImagePath = x.ImagePath,
                    ImagePath2 = x.ImagePath2

                }).Where(x => x.BrandId == brand && x.CategoryId == category).ToList();

                var productCount = Convert.ToDouble(products.Count);
                double pageDouble = productCount / 9;
                double pageRounding = Math.Ceiling(pageDouble);
                int pageCount = Convert.ToInt32(pageRounding);

                ViewBag.PageCount = pageCount;
                ViewBag.Page = page;
                ViewBag.Brand = brand;
                ViewBag.Category = category;

                var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                return View(productsPaged);
            }
            else
            {
                if (sortBy ==1)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == brand && x.CategoryId == category).OrderByDescending(x=> x.Id).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else if (sortBy == 2)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        Stock = x.Stock

                    }).Where(x => x.BrandId == brand && x.CategoryId == category).OrderBy(x=> x.Stock).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else if (sortBy == 3)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == brand && x.CategoryId == category).OrderBy(x=> x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
                else
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.BrandId == brand && x.CategoryId == category).OrderByDescending(x=> x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.Brand = brand;
                    ViewBag.Category = category;
                    ViewBag.SortBy = sortBy;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }
            }

            
        }
    }
}
