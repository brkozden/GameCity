using AutoMapper;
using ETicaretMVC.Models;
using ETicaretMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace ETicaretMVC.Controllers
{
    public class AdminController : Controller
    {
        private AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;
        public AdminController(AppDbContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult Index()
        {
            var waitingOrders = _context.Orders.Where(x => x.Situation == "Sipariş Alındı" && x.Date.Year == DateTime.UtcNow.Year && x.Date.Month == DateTime.UtcNow.Month && x.Date.Day == DateTime.UtcNow.Day).ToList().Count;
            var compeletedOrders = _context.Orders.Where(x => x.Situation == "Teslim Edildi" && x.Date.Year == DateTime.UtcNow.Year && x.Date.Month == DateTime.UtcNow.Month && x.Date.Day == DateTime.UtcNow.Day).ToList().Count;
            var userCount = _context.Users.ToList().Count;
            var totalEarn = _context.Orders.Where(x => x.Date.Year == DateTime.UtcNow.Year && x.Date.Month == DateTime.UtcNow.Month && x.Date.Day == DateTime.UtcNow.Day).ToList();
            decimal earning = 0;
            foreach (var item in totalEarn)
            {
                earning += item.Price;
            }
            
            var fewOrders = _context.Orders.OrderByDescending(x => x.Id).Take(7).ToList();

            var mappedOrders = _mapper.Map<List<OrderViewModel>>(fewOrders);

            ViewBag.WaitingOrders = waitingOrders;
            ViewBag.CompletedOrders = compeletedOrders;
            ViewBag.UserCount = userCount;
            ViewBag.TotalEarn = earning;
            ViewBag.MappedOrders = mappedOrders;

            return View();
        }

        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult OrdersAdmin()
        {
            var join = (from o in _context.Orders
                        join u in _context.Users on o.UserId equals u.Id
                        select new OrderUserViewModel()
                        { 
                            Id= o.Id,
                            Username = u.UserName,
                            Date = o.Date,
                            Email = u.Email,
                            Price= o.Price,
                            Situation= o.Situation,
                            BillId = o.BillId
                             
                        }).ToList();
            var mappedOrders = _mapper.Map<List<OrderUserViewModel>>(join);
            ViewBag.Orders = mappedOrders;
            return View();
        }

        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult OrderDetails(int billId)
        {
            var billInfos = _context.Bills.FirstOrDefault(x => x.Id == billId);
            var billModel = _mapper.Map<BillViewModel>(billInfos);

            var join = (from b in _context.Bills
                        join o in _context.Orders on b.Id equals o.BillId
                        join p in _context.Products on o.ProductId equals p.Id
                        select new BillOrderProductViewModel()
                        {
                            Id = b.Id,
                            ImagePath = p.ImagePath,
                            Price = o.Price,
                            ProductId = o.ProductId,
                            ProductName =o.ProductName
                            
                        }).Where(x=> x.Id == billId).ToList();
            var totalPrice = _context.Bills.FirstOrDefault(x => x.Id == billId);
            ViewBag.TotalPrice = totalPrice.TotalPrice;
            ViewBag.Join = join;

            return View(billModel);
        }

        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult Products()
        {
            var products = _context.Products.ToList();
            var mappedProducts = _mapper.Map<List<ProductViewModel>>(products);
            ViewBag.Products = mappedProducts;
            
            return View();
        }

        public IActionResult RemoveProduct(int productId) 
        {
            var removeProduct = _context.Products.FirstOrDefault(x => x.Id == productId);
            _context.Remove(removeProduct);
            _context.SaveChanges();
            return RedirectToAction("Products");
        }


        [HttpGet]
        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult ProductAdd()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ProductAdd(ProductViewModel newProduct)
        { 
            var product = _mapper.Map<Product>(newProduct);

            if (newProduct.Image != null && newProduct.Image.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var img = root.First(x => x.Name == "img");
                var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName);
                var path = Path.Combine(img.PhysicalPath, randomImageName);
                using var stream = new FileStream(path, FileMode.Create);
                newProduct.Image.CopyTo(stream);
                product.ImagePath = randomImageName;
            }
            if (newProduct.Image2 != null && newProduct.Image2.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var img = root.First(x => x.Name == "img");
                var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image2.FileName);
                var path = Path.Combine(img.PhysicalPath, randomImageName);
                using var stream = new FileStream(path, FileMode.Create);
                newProduct.Image2.CopyTo(stream);
                product.ImagePath2 = randomImageName;
            }
            else
            {
                product.ImagePath2 = "NoImage.jpg";
            }

            if (newProduct.Image3 != null && newProduct.Image3.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var img = root.First(x => x.Name == "img");
                var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image3.FileName);
                var path = Path.Combine(img.PhysicalPath, randomImageName);
                using var stream = new FileStream(path, FileMode.Create);
                newProduct.Image3.CopyTo(stream);
                product.ImagePath3 = randomImageName;
            }
            else
            {
                product.ImagePath3 = "NoImage.jpg";
                    }

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Products");
        }
    }
}
