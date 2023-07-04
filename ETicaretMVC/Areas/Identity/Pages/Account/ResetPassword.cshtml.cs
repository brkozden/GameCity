// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ETicaretMVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace ETicaretMVC.Areas.Identity.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Email { get; set; }

            [DataType(DataType.Password)]
            public string Password { get; set; }

            public string ConfirmPassword { get; set; }

            public string Code { get; set; }

        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                TempData["DoesntExist"] = "Girdiğiniz E-Mail'e ait kullanıcı bulunmamakta.";
                return RedirectToPage("./ForgotPassword");
            }

            if (Input.Password == Input.ConfirmPassword && Input.Password.Length >= 6)
            {
                var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Yeni Şifreniz başarıyla kaydedildi. Giriş yapabilirsiniz.";
                    return RedirectToPage("./Login");
                }
                else
                {
                    TempData["MailError"] = "E-Mailinizi yanlış girdiniz. Lütfen Tekrar Deneyin.";
                    return Page();
                }
            }
            else if (Input.Password != Input.ConfirmPassword)
            {
                TempData["PasswordMatch"] = "Yazmış olduğunuz Yeni Şifre ve Yeni Şifre Tekrar birbiriyle uyuşmuyor.";
                return Page();
            }
            else if (Input.Password.Length < 6)
            {
                TempData["PasswordError"] = "Şifreniz en az 6 karakterden oluşmalıdır.";
            }
            
            
            return Page();
        }
    }
}
