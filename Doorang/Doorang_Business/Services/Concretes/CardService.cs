using Doorang_Business.Exceptions.Card;
using Doorang_Business.Services.Abstracts;
using Doorang_Core.Models;
using Doorang_Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doorang_Business.Services.Concretes
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public void AddCard(Card card)
        {
            if (card == null) throw new NullReferenceException("Card not found");

            if (!card.ImgFile.ContentType.Contains("image/"))
                throw new FileContentTypeException("ImageFile", "File content type error");
            if (card.ImgFile.Length > 2097152)
                throw new FileSizeException("Image", "File size error");

            string filename = card.ImgFile.FileName;
            string path = @"C:\Users\Asus\Desktop\MVCProject\Doorang\Doorang\wwwroot\upload\card\" + filename;
            using(FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                card.ImgFile.CopyTo(fileStream);
            }
            card.ImgUrl = filename;

            _cardRepository.Add(card);
            _cardRepository.Commit();
        }
        public void DeleteCard(int id)
        {
            var existCard = _cardRepository.Get(x => x.Id == id);
            if (existCard == null) throw new EntityNotFoundException("", "Entity not found");

            string path = @"C:\Users\Asus\Desktop\MVCProject\Doorang\Doorang\wwwroot\upload\card\" + existCard.ImgUrl;
            if (!File.Exists(path))
                throw new Exceptions.Card.FileNotFoundException("ImageFile", "File not found");

            File.Delete(path);

            _cardRepository.Delete(existCard);
            _cardRepository.Commit();
        }

        public List<Card> GetAllCards(Func<Card, bool>? func = null)
        {
            return _cardRepository.GetAll(func);
        }


        public Card GetCard(Func<Card, bool>? func = null)
        {
            return _cardRepository.Get(func);
        }

        public void UpdateCard(int id, Card newCard)
        {
            var existCard = _cardRepository.Get(x => x.Id == id);
            if (existCard == null) throw new EntityNotFoundException("", "Entity not found");

            if(newCard.ImgFile != null)
            {
                if (!newCard.ImgFile.ContentType.Contains("image/"))
                    throw new FileContentTypeException("ImageFile", "File content type error");
                if (newCard.ImgFile.Length > 2097152)
                    throw new FileSizeException("Image", "File size error");

                string filename = newCard.ImgFile.FileName;
                string path = @"C:\Users\Asus\Desktop\MVCProject\Doorang\Doorang\wwwroot\upload\card\" + filename;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    newCard.ImgFile.CopyTo(fileStream);
                }
                newCard.ImgUrl = filename;

                existCard.ImgUrl = newCard.ImgUrl;
            }

            existCard.Title = newCard.Title;
            existCard.SubTitle = newCard.SubTitle;
            existCard.Description = newCard.Description;

            _cardRepository.Commit();
        }
    }
}
