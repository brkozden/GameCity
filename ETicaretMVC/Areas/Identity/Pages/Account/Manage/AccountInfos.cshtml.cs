using ETicaretMVC.Areas.Identity.Data;
using ETicaretMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ETicaretMVC.Areas.Identity.Pages.Account.Manage
{
    public class AccountInfosModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private AppDbContext _context;

        public AccountInfosModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Name { get; set; }
        public string Surname { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {

            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [Display(Name = "Confirm new password")]
            public string ConfirmPassword { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            var modelname = user.Name;
            var modelsurName = user.Surname;
            var basketCount = _context.Baskets.Where(x => x.UserId == user.Id).Count();
            ViewData["BasketCount"] = basketCount;
            Name = modelname;
            Surname = modelsurName;

        }

        public async Task<IActionResult> OnGetAsync()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            TempData["Username"] = user.UserName;
            await LoadAsync(user);
            TempData["Name"] = Name;
            TempData["Surname"] = Surname;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Input.Name != null && Input.Surname != null)
            {
                user.Name = Input.Name;
                user.Surname = Input.Surname;
                _context.SaveChanges();
                TempData["ChangedNameSurname"] = "�sim ve Soyisminiz Ba�ar�yla De�i�tirildi";
            }
            else if (Input.Name != null && Input.Surname == null)
            {
                user.Name = Input.Name;
                _context.SaveChanges();
                TempData["ChangedName"] = "�sminiz Ba�ar�yla De�i�tirildi";
            }
            else if (Input.Surname != null && Input.Name == null)
            {
                user.Surname = Input.Surname;
                _context.SaveChanges();
                TempData["ChangedSurname"] = "Soyisminiz Ba�ar�yla De�i�tirildi";
            }
            if (Input.NewPassword != null)
            {
                if (Input.NewPassword == Input.ConfirmPassword && Input.NewPassword.Length >= 6)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);

                    if (!changePasswordResult.Succeeded)
                    {
                        TempData["WrongPassword"] = "Yazd���n�z Eski �ifre'niz Do�ru De�il.L�tfen Tekrar Deneyin.";
                        return RedirectToPage();
                    }

                    await _signInManager.RefreshSignInAsync(user);
                    TempData["Success"] = "�ifreniz Ba�ar�yla De�i�tirildi";
                    return RedirectToPage();
                }
                else if (Input.NewPassword != Input.ConfirmPassword)
                {
                    TempData["ConfirmationError"] = "Yazd���n�z Yeni �ifre ile Yeni �ifre Tekrar uyu�muyor.L�tfen Tekrar Deneyin.";
                    return RedirectToPage();
                }
                else if (Input.NewPassword.Length < 6)
                {
                    TempData["PasswordError"] = "Yeni �ifre'niz en az 6 karakterden olu�mal�.";
                    return RedirectToPage();
                }
                else
                {
                    return RedirectToPage();
                }
            }
            else
            {
                return RedirectToPage();
            }
            
        }
    }
}
