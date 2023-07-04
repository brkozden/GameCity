using AutoMapper;
using ETicaretMVC.Areas.Identity.Data;
using ETicaretMVC.Models;
using ETicaretMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretMVC.Controllers
{
    public class ContactController : Controller
    {
        private AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public ContactController(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager)
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

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["BasketCount"] = getBasketCount();
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactMessageViewModel Contact)
        {
            var message = _mapper.Map<ContactMessage>(Contact);

            _context.ContactMessages.Add(message);
            _context.SaveChanges();

            ViewBag.Message = "sent";

            return View();
        }
    }
}
