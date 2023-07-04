// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ETicaretMVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using ETicaretMVC.Models;
using System.Net.Mail;

namespace ETicaretMVC.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<AppUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);

            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                TempData["DoesntExist"] = "Bu E-Mail ile kayıtlı bir kullanıcı bulunmamakta.";
                return RedirectToPage("./ForgotPassword");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                MailAddress sendMail = new MailAddress("gamecitynoreply@gmail.com");
                MailMessage message = new MailMessage();
                message.To.Add(user.Email);
                message.From = sendMail;
                message.Subject = "GameCity Şifremi Unuttum";
                message.Body = "Şifrenizi değiştirmek için gönderiğimiz linke basınız : " + HtmlEncoder.Default.Encode(callbackUrl);

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new System.Net.NetworkCredential("gamecitynoreply@gmail.com", "jicbgyrprnquksxv");
                client.EnableSsl = true;
                client.Send(message);
            
                TempData["Information"] = "E-Mail adresinize şifrenizi sıfırlayabileceğiniz bir bağlantı yolladık.";
                return RedirectToPage("./Login");

        }
    }
}
