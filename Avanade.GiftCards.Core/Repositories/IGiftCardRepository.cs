using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avanade.GiftCards.Core.Entities;

namespace Avanade.GiftCards.Core.Repositories
{
    public interface IGiftCardRepository
    {
        IList<GiftCard> FetchAll();
        void Create(GiftCard giftCard);
        void Update(GiftCard oldGiftCard, GiftCard newGiftCard);
        void Delete(GiftCard giftCard);
    }
}
