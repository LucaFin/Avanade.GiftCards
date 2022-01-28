using Avanade.GiftCards.Core.Entities;
using Avanade.GiftCards.Core.Repositories;
using Avanade.GiftCards.Mock.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avanade.GiftCards.Mock.Repository
{
    public class GiftCardRepositoryMock : IGiftCardRepository
    {
        public void Create(GiftCard giftCard)
        {
            var newId = AllocationMockStorage.GiftCards.Max(e => e.Id) + 1;
            giftCard.Id = newId;
            AllocationMockStorage.GiftCards.Add(giftCard);
        }

        public void Delete(GiftCard giftCard)
        {
            var existingGiftCard = AllocationMockStorage.GiftCards.FirstOrDefault(x => x.Id == giftCard.Id);
            AllocationMockStorage.GiftCards.Remove(existingGiftCard);
        }

        public IList<GiftCard> FetchAll()
        {
            return AllocationMockStorage.GiftCards.OrderBy(x => x.DataDiScadenza).ToList();
        }

        public void Update(GiftCard oldGiftCard, GiftCard newGiftCard)
        {
            var existingGiftCard = AllocationMockStorage.GiftCards.FirstOrDefault(x => x.Id == oldGiftCard.Id);
            AllocationMockStorage.GiftCards.Remove(existingGiftCard);
            AllocationMockStorage.GiftCards.Add(newGiftCard);
        }
    }
}
