
using Doorang_Business.Services.Abstracts;
using Doorang_Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Doorang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICardService _cardService;

        public HomeController(ICardService cardService)
        {
            _cardService = cardService;
        }

        public IActionResult Index()
        {
            List<Card> cards = _cardService.GetAllCards();
            return View(cards);
        }

        
    }
}
