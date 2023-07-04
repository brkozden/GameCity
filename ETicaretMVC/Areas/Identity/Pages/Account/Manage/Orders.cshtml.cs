using AutoMapper;
using ETicaretMVC.Areas.Identity.Data;
using ETicaretMVC.Models;
using ETicaretMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ETicaretMVC.Areas.Identity.Pages.Account.Manage
{
    public class OrdersModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrdersModel(
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _mapper = mapper;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var basketCount = _context.Baskets.Where(x => x.UserId == user.Id).Count();
            ViewData["BasketCount"] = basketCount;
            string UserId = user.Id;

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var orders = _context.Orders.Where(x => x.UserId == UserId).ToList();
            var mappedOrder = _mapper.Map<List<OrderUserViewModel>>(orders);
            TempData["Orders"] = mappedOrder;
            TempData["Username"] = user.UserName;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            return RedirectToPage();
        }
    }
}
