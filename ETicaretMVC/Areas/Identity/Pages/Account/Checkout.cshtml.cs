using AutoMapper;
using ETicaretMVC.Areas.Identity.Data;
using ETicaretMVC.Models;
using ETicaretMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ETicaretMVC.Areas.Identity.Pages.Account
{
    public class CheckoutModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CheckoutModel(IMapper mapper, AppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public decimal totalPrice = 0;

        public class InputModel
        {
            public string UserId { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            var basket = _context.Baskets.Where(x => x.UserId == user.Id).ToList();

            foreach (var item in basket)
            {
                totalPrice += item.Price;
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {

            var user = await _userManager.GetUserAsync(User);

            var basket = _context.Baskets.Where(x => x.UserId == user.Id).ToList();
            var products = _mapper.Map<List<BasketViewModel>>(basket);
            TempData["Products"] = products;
            foreach (var item in basket)
            {
                totalPrice += item.Price;
            }
            TempData["TotalPrice"] = totalPrice;

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string payment)
        {

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await LoadAsync(user);

            var newBill = new Bill { UserId = user.Id, Name = Input.Name, Surname = Input.Surname, Address = Input.Address, Email = Input.Email, PhoneNumber = Input.PhoneNumber, TotalPrice = totalPrice , Date = DateTime.UtcNow , PaymentMethod=payment};
            _context.Bills.Add(newBill);
            _context.SaveChanges();

            var removeFromBasket = _context.Baskets.Where(x => x.UserId == user.Id).ToList();
            foreach (var item in removeFromBasket)
            {
                var addOrder = new Orders { UserId = user.Id, Price = item.ProductPrice, Date = DateTime.UtcNow, Situation = "Sipariþ Alýndý",ProductId = item.ProductId, ProductName = item.ProductName ,BillId = newBill.Id};
                _context.Orders.Add(addOrder);
                _context.Remove(item);
                var product = _context.Products.FirstOrDefault(x => x.Id == item.ProductId);
                product.Stock = product.Stock - item.Amount;
            }
            
            
            
            _context.SaveChanges();
            TempData["Success"] = "Sipariþiniz baþarýyla alýndý. Ürünlerinizi en kýsa sürede sizlere ulaþtýracaðýz.";

            return RedirectToPage();
        }

    }
}
