using Avanade.GiftCards.Core.Entities;
using Avanade.GiftCards.Core.Repositories;
using Avanade.GiftCards.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avanade.GiftCards.Core.BusinessLayer
{
    public class MainBusinessLayer
    {
        private IGiftCardRepository _giftCardRepository;

        public MainBusinessLayer(IGiftCardRepository giftCardRepository)
        {
            _giftCardRepository = giftCardRepository;
        }

        public IList<GiftCard> FetchAllGiftCards()
        {
            return _giftCardRepository.FetchAll();
        }

        public Response CreateGiftCard(GiftCard giftCard)
        {
            if (giftCard == null)
            {
                return new Response { Success = false, Message = "GiftCard non valida" };
            }
               if (giftCard.DataDiScadenza < DateTime.Today)
               {
                   return new Response { Success = false, Message = "Non puoi creare GiftCard scadute" };
               }
               _giftCardRepository.Create(giftCard);
               return new Response { Success = true, Message = $"GiftCard: {giftCard.Id} creata" };
           }

           public Response UpdateGiftCard(GiftCard giftCard)
           {
               if (giftCard == null)
               {
                   return new Response { Success = false, Message = "GiftCard non valida" };
               }
               if (giftCard.Id < 0)
               {
                   return new Response { Success = false, Message = "ID della GiftCard non valido" };
               }
               if (giftCard.DataDiScadenza < DateTime.Today)
               {
                   return new Response { Success = false, Message = "Non puoi creare GiftCard scadute" };
               }
            var gitfCardToUpdate = FetchAllGiftCards().FirstOrDefault(x => x.Id == giftCard.Id);
            _giftCardRepository.Update(gitfCardToUpdate, giftCard);
            return new Response() { Success = true, Message = "GiftCard Aggiornata con successo" };
        }

        public Response DeleteGiftCard(GiftCard giftCard)
        {
            if (giftCard == null)
            {
                return new Response { Success = false, Message = "GiftCard non valida" };
            }
            if (giftCard.Id < 0)
            {
                return new Response { Success = false, Message = "ID della GiftCard non valido" };
            }
            var gitfCardToDelete = FetchAllGiftCards().FirstOrDefault(x => x.Id == giftCard.Id);
            _giftCardRepository.Delete(gitfCardToDelete);
            return new Response { Success = true, Message = $"GiftCard: {giftCard.Id} cancellata con successo" };
        }
    }
}
