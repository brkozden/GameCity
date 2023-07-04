using ETicaretMVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ETicaretMVC.Models;

namespace ETicaretMVC.Areas.Identity.Pages.Account.Manage
{
    public class MyAddressModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private AppDbContext _context;

        public MyAddressModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
        }

        public string Email { get; set; }
        public string Address { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "New email")]
            public string NewEmail { get; set; }

            public string Address { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            var adres = user.Address;
            var basketCount = _context.Baskets.Where(x => x.UserId == user.Id).Count();
            ViewData["BasketCount"] = basketCount;

            Address = adres;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            TempData["Username"] = user.UserName;
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            else if (Input.Address != null)
            {
                user.Address = Input.Address;
                _context.SaveChanges();
                TempData["Success"] = "Adresiniz Baþarýyla Deðiþtirildi.";
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage();
            }



        }
    }
}
