using AutoMapper;
using ETicaretMVC.Areas.Identity.Data;
using ETicaretMVC.Models;
using ETicaretMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretMVC.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductDetailsController(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
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

        public IActionResult Index(int id)
        {
            ViewData["BasketCount"] = getBasketCount();
            var join = (from c in _context.Categories
                        join p in _context.Products on c.Id equals p.CategoryId
                        join b in _context.Brands on p.BrandId equals b.Id
                        join g in _context.GeneralCategories on p.GeneralCategoryId equals g.Id
                        select new ProductGeneralCategoryBrandCategoryJoinViewModel()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            Stock = p.Stock,
                            ImagePath = p.ImagePath,
                            ImagePath2 = p.ImagePath2,
                            ImagePath3 = p.ImagePath3,
                            BrandName = b.Name,
                            CategoryName = c.Name,
                            GeneralCategoryName = g.Name,
                            GeneralCategoryId = g.Id,
                            CategoryId = c.Id,
                            BrandId = b.Id

                        }).ToList();

            if (TempData["id"]== null )
            {
                var userComments = _context.Comments.Where(x => x.ProductId == id).ToList();
                var comments = _mapper.Map<List<CommentViewModel>>(userComments);
                ViewBag.Comments = comments;
                int starAverage = 0;
                foreach (var item in userComments)
                {
                    starAverage += item.StarCount;
                }
                if (userComments.Count !=0)
                {
                    starAverage = starAverage / userComments.Count;
                }
                ViewBag.StarCount = starAverage;
                ViewBag.CommentCount = userComments.Count;
                var product = join.Find(x => x.Id == id);
                return View(product);
            }
            else
            {
                id =(int) TempData["id"];
                var product = join.Find(x => x.Id == id);
                var userComments = _context.Comments.Where(x => x.ProductId == id).ToList();
                var comments = _mapper.Map<List<CommentViewModel>>(userComments);
                int starAverage = 0;
                foreach (var item in userComments)
                {
                    starAverage += item.StarCount;
                }
                if (userComments.Count != 0)
                {
                    starAverage = starAverage / userComments.Count;
                }
                ViewBag.CommentCount = userComments.Count;
                ViewBag.StarCount = starAverage;
                ViewBag.Comments = comments;
                return View(product);
            }  
        }

        [HttpGet]
        public IActionResult NewComment(int id)
        {
            ViewData["BasketCount"] = getBasketCount();
            ViewBag.Star = new List<int>()
            {
                5,4,3,2,1
            };

            var oldComments = _context.Comments.Where(x => x.ProductId == id).ToList();
            var userComments = _mapper.Map<List<CommentViewModel>>(oldComments);

            ViewBag.OldComments = userComments;
            TempData["newComment"] = id;
            TempData["id"] = id;
            return View();
        }

        [HttpPost]
        public IActionResult NewComment(CommentViewModel newComment)
        {
            var addComment = _mapper.Map<Comment>(newComment);

            ViewBag.Star = new List<int>()
            {
                5,4,3,2,1
            };

            addComment.Created = DateTime.Now;
            addComment.ProductId = (int)TempData["newComment"];

            _context.Comments.Add(addComment);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket(int productId,int amount, int which,int page, string searchText, int category, int brand, string name, int generalCategory)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Redirect("~/Identity/Account/Login");
            }
            else
            {
                var productInfos = _context.Products.FirstOrDefault(x => x.Id == productId);
                var newBasket = new Basket { ProductId = productId, UserId = user.Id, Price = amount * productInfos.Price, Amount = amount, ProductImagePath = productInfos.ImagePath, ProductName = productInfos.Name, ProductPrice = productInfos.Price };
                _context.Baskets.Add(newBasket);
                _context.SaveChanges();
            }
            
            if (which == 1)
            {
                return RedirectToAction("index", new { id = productId }) ;
            }
            else if (which==2)
            {
                return RedirectToAction("Shop", "Home",new { page = page});
            }
            else if (which==3)
            {
                return RedirectToAction("Pagination", "Home", new {searchText= searchText , page= page});
            }
            else if (which == 4)
            {
                return RedirectToAction("Categories", "Home", new { category = category, page = page });
            }
            else if (which == 5)
            {
                return RedirectToAction("Pagination", "Home", new { searchText = searchText, page = page });
            }
            else if (which == 6)
            {
                return RedirectToAction("Slider", "Mainpage", new { page = page, brand = brand, category = category, name = name });
            }
            else if (which == 7)
            {
                return RedirectToAction("GeneralCategory", "Mainpage", new { generalCategory = generalCategory, page = page });
            }
            else if (which == 8)
            {
                return RedirectToAction("CampaignSingle", "Mainpage", new { page = page });
            }
            else if (which == 9)
            {
                return RedirectToAction("Brands", "Mainpage", new { brand = brand, page = page });
            }
            else if (which == 10)
            {
                return RedirectToAction("Campaigns", "Mainpage", new { brand = brand, page = page, category = category });
            }
            return RedirectToAction("index","Mainpage");
        }

        [HttpGet]
        public async Task<IActionResult> AddToFavorites(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("~/Identity/Account/Login");
            }
            else
            {
                var productInfos = _context.Products.FirstOrDefault(x => x.Id == productId);
                var newFavorite = new Favorite { ProductId = productId, UserId = user.Id, ProductImagePath = productInfos.ImagePath, ProductName =productInfos.Name, ProductPrice = productInfos.Price, ProductStock = productInfos.Stock };
                _context.Favorites.Add(newFavorite);
                _context.SaveChanges();
                return RedirectToAction("index", new { id = productId });
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int productId, int which, int page, string searchText, int category,int brand ,string name, int generalCategory)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Redirect("~/Identity/Account/Login");
            }
            else 
            {
                var productInfos = _context.Products.FirstOrDefault(x => x.Id == productId);
                var newFavorite = new Favorite { ProductId = productId, UserId = user.Id, ProductImagePath = productInfos.ImagePath, ProductName = productInfos.Name, ProductPrice = productInfos.Price, ProductStock = productInfos.Stock };
                _context.Favorites.Add(newFavorite);
                _context.SaveChanges();
            }

            if (which == 2)
            {
                return RedirectToAction("Shop", "Home", new { page = page });
            }
            else if (which == 3)
            {
                return RedirectToAction("Pagination", "Home", new { searchText = searchText, page = page });
            }
            else if (which == 4)
            {
                return RedirectToAction("Categories", "Home", new { category = category, page = page });
            }
            else if (which == 5)
            {
                return RedirectToAction("Pagination", "Home", new { searchText = searchText, page = page });
            }
            else if (which == 6)
            {
                return RedirectToAction("Slider", "Mainpage", new { page = page, brand = brand , category = category, name=name });
            }
            else if (which == 7)
            {
                return RedirectToAction("GeneralCategory", "Mainpage", new { generalCategory = generalCategory, page = page });
            }
            else if (which == 8)
            {
                return RedirectToAction("CampaignSingle", "Mainpage", new { page = page });
            }
            else if (which == 9)
            {
                return RedirectToAction("Brands", "Mainpage", new { brand = brand, page = page });
            }
            else if (which == 10)
            {
                return RedirectToAction("Campaigns", "Mainpage", new {brand = brand, page = page , category = category});
            }
            return RedirectToAction("index", "Mainpage");
        }

        public async Task<IActionResult> AddToBasketFromFavorites(int favoriteId, int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("~/Identity/Account/Login");
            }
            else
            {
                var productInfos = _context.Products.FirstOrDefault(x => x.Id == productId);
                var newBasket = new Basket { ProductId = productId, UserId = user.Id, Price =productInfos.Price, Amount = 1, ProductImagePath = productInfos.ImagePath, ProductName = productInfos.Name, ProductPrice = productInfos.Price };
                _context.Baskets.Add(newBasket);

                var favorite = _context.Favorites.FirstOrDefault(x => x.Id == favoriteId);
                _context.Favorites.Remove(favorite);
                _context.SaveChanges();

                return Redirect("~/Identity/Account/Favorites");
            }

            
        }
    }
}
