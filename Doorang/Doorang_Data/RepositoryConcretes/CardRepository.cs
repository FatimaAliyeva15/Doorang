
using Doorang_Core.Models;
using Doorang_Core.RepositoryAbstracts;
using Doorang_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doorang_Data.RepositoryConcretes
{
    public class CardRepository : GenericRepository<Card>, ICardRepository
    {
        public CardRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
