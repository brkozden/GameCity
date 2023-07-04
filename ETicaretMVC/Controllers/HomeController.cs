using AutoMapper;
using ETicaretMVC.Areas.Identity.Data;
using ETicaretMVC.Models;
using ETicaretMVC.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Mail;

namespace ETicaretMVC.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager)
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
        public IActionResult Index()
        {

            ViewData["BasketCount"] = getBasketCount();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult SearchTextDirector(string searchText)
        {
            return RedirectToAction("Pagination", new { searchText = searchText, page = 1 });
        }

        public IActionResult Pagination(string searchText, int page, int category, int sortBy)
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


            if (searchText != null && category == 0 && sortBy == 0)
            {
                var products = _context.Products.Select(x => new SearchViewModel
                {
                    Name = x.Name.Replace("I", "i"),
                    Description = x.Description,
                    Id = x.Id,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                    ImagePath2 = x.ImagePath2

                }).Where(x => x.Name.Contains(searchText)).ToList();

                var productCount = Convert.ToDouble(products.Count);
                double pageDouble = productCount / 9;
                double pageRounding = Math.Ceiling(pageDouble);
                int pageCount = Convert.ToInt32(pageRounding);

                var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                ViewBag.SortBy = sortBy;
                ViewBag.Category = category;
                ViewBag.SearchText = searchText;
                ViewBag.PageCount = pageCount;
                ViewBag.Page = page;

                return View(productsPaged);
            }
            else if (searchText != null && category != 0 && sortBy == 0)
            {
                var products = _context.Products.Select(x => new SearchViewModel
                {
                    Name = x.Name.Replace("I", "i"),
                    Description = x.Description,
                    Id = x.Id,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                    ImagePath2 = x.ImagePath2,
                    CategoryId = x.CategoryId

                }).Where(x => x.Name.Contains(searchText) && x.CategoryId == category).ToList();

                var productCount = Convert.ToDouble(products.Count);
                double pageDouble = productCount / 9;
                double pageRounding = Math.Ceiling(pageDouble);
                int pageCount = Convert.ToInt32(pageRounding);

                var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                ViewBag.SortBy = sortBy;
                ViewBag.Category = category;
                ViewBag.SearchText = searchText;
                ViewBag.PageCount = pageCount;
                ViewBag.Page = page;

                return View(productsPaged);
            }
            else if (searchText != null && category == 0 && sortBy != 0)
            {
                if (sortBy == 1)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name.Replace("I", "i"),
                        Description = x.Description,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        CategoryId = x.CategoryId

                    }).Where(x => x.Name.Contains(searchText)).OrderByDescending(x => x.Id).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    ViewBag.SearchText = searchText;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;

                    return View(productsPaged);
                }
                else if (sortBy == 2)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name.Replace("I", "i"),
                        Description = x.Description,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        CategoryId = x.CategoryId,
                        Stock = x.Stock

                    }).Where(x => x.Name.Contains(searchText)).OrderBy(x => x.Stock).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    ViewBag.SearchText = searchText;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;

                    return View(productsPaged);
                }
                else if (sortBy == 3)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name.Replace("I", "i"),
                        Description = x.Description,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        CategoryId = x.CategoryId

                    }).Where(x => x.Name.Contains(searchText)).OrderBy(x => x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    ViewBag.SearchText = searchText;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;

                    return View(productsPaged);
                }
                else if (sortBy == 4)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name.Replace("I", "i"),
                        Description = x.Description,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        CategoryId = x.CategoryId

                    }).Where(x => x.Name.Contains(searchText)).OrderByDescending(x => x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    ViewBag.SearchText = searchText;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;

                    return View(productsPaged);
                }

            }
            else if (searchText == null && category != 0 && sortBy != 0)
            {
                if (sortBy == 1)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name.Replace("I", "i"),
                        Description = x.Description,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        CategoryId = x.CategoryId

                    }).Where(x => x.CategoryId == category).OrderByDescending(x => x.Id).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    ViewBag.SearchText = searchText;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;

                    return View(productsPaged);
                }
                else if (sortBy == 2)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name.Replace("I", "i"),
                        Description = x.Description,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        CategoryId = x.CategoryId,
                        Stock = x.Stock

                    }).Where(x => x.CategoryId == category).OrderBy(x => x.Stock).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    ViewBag.SearchText = searchText;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;

                    return View(productsPaged);
                }
                else if (sortBy == 3)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name.Replace("I", "i"),
                        Description = x.Description,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        CategoryId = x.CategoryId

                    }).Where(x => x.CategoryId == category).OrderBy(x => x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    ViewBag.SearchText = searchText;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;

                    return View(productsPaged);
                }
                else if (sortBy == 4)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name.Replace("I", "i"),
                        Description = x.Description,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        CategoryId = x.CategoryId

                    }).Where(x => x.CategoryId == category).OrderByDescending(x => x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    ViewBag.SearchText = searchText;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;

                    return View(productsPaged);
                }
            }
            else if (searchText != null && category != 0 && sortBy != 0)
            {
                if (sortBy == 1)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name.Replace("I", "i"),
                        Description = x.Description,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        CategoryId = x.CategoryId

                    }).Where(x => x.CategoryId == category && x.Name.Contains(searchText)).OrderByDescending(x => x.Id).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    ViewBag.SearchText = searchText;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;

                    return View(productsPaged);
                }
                else if (sortBy == 2)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name.Replace("I", "i"),
                        Description = x.Description,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        CategoryId = x.CategoryId,
                        Stock = x.Stock

                    }).Where(x => x.CategoryId == category && x.Name.Contains(searchText)).OrderBy(x => x.Stock).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    ViewBag.SearchText = searchText;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;

                    return View(productsPaged);
                }
                else if (sortBy == 3)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name.Replace("I", "i"),
                        Description = x.Description,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        CategoryId = x.CategoryId

                    }).Where(x => x.CategoryId == category && x.Name.Contains(searchText)).OrderBy(x => x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    ViewBag.SearchText = searchText;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;

                    return View(productsPaged);
                }
                else if (sortBy == 4)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name.Replace("I", "i"),
                        Description = x.Description,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        CategoryId = x.CategoryId

                    }).Where(x => x.CategoryId == category && x.Name.Contains(searchText)).OrderByDescending(x => x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    ViewBag.SortBy = sortBy;
                    ViewBag.Category = category;
                    ViewBag.SearchText = searchText;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;

                    return View(productsPaged);
                }
            }
            else
            {
                var products = _context.Products.Select(x => new SearchViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Id = x.Id,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                    ImagePath2 = x.ImagePath2

                }).ToList();

                var productCount = Convert.ToDouble(products.Count);
                double pageDouble = productCount / 9;
                double pageRounding = Math.Ceiling(pageDouble);
                int pageCount = Convert.ToInt32(pageRounding);

                var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                ViewBag.Category = category;
                ViewBag.SortBy = sortBy;
                ViewBag.SearchText = searchText;
                ViewBag.PageCount = pageCount;
                ViewBag.Page = page;

                return View(productsPaged);
            }
            return View();
        }

        public IActionResult Shop(int page, int sortBy)
        {
            ViewData["BasketCount"] = getBasketCount();

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

            if (sortBy == 0)
            {
                var products = _context.Products.Select(x => new SearchViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                    ImagePath2 = x.ImagePath2

                }).ToList();

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
                if (sortBy == 1)
                {
                    var products = _context.Products.Select(x => new SearchViewModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).OrderByDescending(x => x.Id).ToList();

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
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2,
                        Stock = x.Stock

                    }).OrderBy(x => x.Stock).ToList();

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
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).OrderBy(x => x.Price).ToList();

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
                        ImagePath = x.ImagePath,
                        ImagePath2 = x.ImagePath2

                    }).OrderByDescending(x => x.Price).ToList();

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

        [HttpPost]
        public IActionResult Filter(int category, int brand, string searchText, int sortBy, string name, int generalCategory)
        {

            if (brand != 0 && name == null)
            {
                return RedirectToAction("Brands", "Mainpage", new { page = 1, category = category, brand = brand, sortBy = sortBy });
            }
            else if (searchText != null)
            {
                return RedirectToAction("Pagination", new { page = 1, category = category, searchText = searchText, sortBy = sortBy });
            }
            else if (name != null)
            {
                return RedirectToAction("Slider", "Mainpage", new { page = 1, brand = brand, category = category, name = name, sortBy = sortBy });
            }
            else if (generalCategory != 0)
            {
                return RedirectToAction("GeneralCategory", "Mainpage", new { page = 1, generalCategory = generalCategory, sortBy = sortBy, category = category });
            }
            else if (category != 0)
            {
                return RedirectToAction("Categories", new { page = 1, category = category, sortBy = sortBy });
            }
            else
            {
                return RedirectToAction("Shop", new { page = 1, sortBy = sortBy });
            }


        }

        [HttpPost]
        public IActionResult FilterForCampaigns(int category, int brand, int sortBy)
        {
            if (brand != 0)
            {
                return RedirectToAction("Campaigns", "Mainpage", new { page = 1, category = category, brand = brand, sortBy = sortBy });
            }
            else
            {
                return RedirectToAction("CampaingSingle", "Mainpage", new { page = 1, sortBy = sortBy });
            }
        }
        public IActionResult Email(string email)
        {
            ViewData["BasketCount"] = getBasketCount();
            var emailAddress = _context.Emails.FirstOrDefault(x => x.Emails == email);

            if (emailAddress != null)
            {
                ViewBag.Email = emailAddress;
                return RedirectToAction("index");
            }
            else
            {
                MailAddress sendMail = new MailAddress("gamecitynoreply@gmail.com");

                MailMessage message = new MailMessage();
                message.To.Add(email);
                message.From = sendMail;
                message.Subject = "GameCity E-Bülteni";
                message.Body = "GameCity E-Bültenimize başarıyla kayıt oldunuz. Gelecekte olacak haberleri ve indirimleri size en kısa sürede bildireceğiz.";

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new System.Net.NetworkCredential("gamecitynoreply@gmail.com", "jicbgyrprnquksxv");
                client.EnableSsl = true;
                client.Send(message);

                var newEmail = new Email() { Emails = email };
                _context.Emails.Add(newEmail);
                _context.SaveChanges();

                return RedirectToAction("index");


            }

        }

        public IActionResult Categories(int category, int page, int sortBy)
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

            if (sortBy == 0)
            {
                var products = _context.Products.Select(x => new SearchViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    ImagePath = x.ImagePath,
                    ImagePath2 = x.ImagePath2

                }).Where(x => x.CategoryId == category).ToList();

                var productCount = Convert.ToDouble(products.Count);
                double pageDouble = productCount / 9;
                double pageRounding = Math.Ceiling(pageDouble);
                int pageCount = Convert.ToInt32(pageRounding);

                ViewBag.PageCount = pageCount;
                ViewBag.Page = page;
                ViewBag.Category = category;

                var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                return View(productsPaged);
            }
            else
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
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.CategoryId == category).OrderByDescending(x => x.Id).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.Category = category;

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
                        Stock = x.Stock

                    }).Where(x => x.CategoryId == category).OrderBy(x => x.Stock).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.Category = category;

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
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.CategoryId == category).OrderBy(x => x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.Category = category;

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
                        ImagePath2 = x.ImagePath2

                    }).Where(x => x.CategoryId == category).OrderByDescending(x => x.Price).ToList();

                    var productCount = Convert.ToDouble(products.Count);
                    double pageDouble = productCount / 9;
                    double pageRounding = Math.Ceiling(pageDouble);
                    int pageCount = Convert.ToInt32(pageRounding);

                    ViewBag.SortBy = sortBy;
                    ViewBag.PageCount = pageCount;
                    ViewBag.Page = page;
                    ViewBag.Category = category;

                    var productsPaged = products.Skip((page - 1) * 9).Take(9).ToList();

                    return View(productsPaged);
                }

            }
        }

        public IActionResult RemoveProductFromBasket(int id)
        {
            var remove = _context.Baskets.FirstOrDefault(x => x.Id == id);
            _context.Baskets.Remove(remove);
            _context.SaveChanges();
            return Redirect("~/Identity/Account/Basket");
        }

        public IActionResult RemoveProductFromFavorites(int id)
        {
            var remove = _context.Favorites.FirstOrDefault(x => x.Id == id);
            _context.Favorites.Remove(remove);
            _context.SaveChanges();
            return Redirect("~/Identity/Account/Favorites");
        }

        public IActionResult IncreaseAmount(int id)
        {
            var inc = _context.Baskets.FirstOrDefault(x => x.Id == id);
            inc.Amount++;
            inc.Price = inc.ProductPrice * inc.Amount;
            _context.Baskets.Update(inc);
            _context.SaveChanges();
            return Redirect("~/Identity/Account/Basket");
        }

        public IActionResult DecreaseAmount(int id)
        {
            var dec = _context.Baskets.FirstOrDefault(x => x.Id == id);
            if (dec.Amount == 1)
            {
                _context.Baskets.Remove(dec);
                _context.SaveChanges();
                return Redirect("~/Identity/Account/Basket");
            }
            else
            {
                dec.Amount--;
                dec.Price = dec.ProductPrice * dec.Amount;
                _context.Baskets.Update(dec);
                _context.SaveChanges();
                return Redirect("~/Identity/Account/Basket");

            }



        }



    }
}