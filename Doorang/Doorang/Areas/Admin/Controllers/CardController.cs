using Doorang_Business.Exceptions.Card;
using Doorang_Business.Services.Abstracts;
using Doorang_Core.Models;
using Microsoft.AspNetCore.Mvc;
using FileNotFoundException = Doorang_Business.Exceptions.Card.FileNotFoundException;

namespace Doorang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        public IActionResult Index()
        {
            List<Card> cards = _cardService.GetAllCards();
            return View(cards);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Card card)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _cardService.AddCard(card);
            }
            catch(FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(NullReferenceException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var existCard = _cardService.GetCard(x => x.Id == id);
            if (existCard == null) return NotFound();

            try
            {
                _cardService.DeleteCard(id);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var employee = _cardService.GetCard(x => x.Id == id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Update(int id, Card card)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _cardService.UpdateCard(id, card);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
