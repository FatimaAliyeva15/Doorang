using Doorang_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doorang_Business.Services.Abstracts
{
    public interface ICardService
    {
        void AddCard(Card card);
        void DeleteCard(int id);
        void UpdateCard(int id, Card card);
        Card GetCard(Func<Card, bool>? func = null);
        List<Card> GetAllCards(Func<Card, bool>? func = null);
    }
}
