using Avanade.GiftCards.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avanade.GiftCards.Mock.Storage
{
    public class AllocationMockStorage
    {
        public static IList<GiftCard> GiftCards { get; set; }
        public static void Initialize()
        {
            GiftCards = new List<GiftCard>();
            GiftCards.Add(
                new GiftCard
                {
                    Id = 1,
                    Mittente = "Tony Stark",
                    Destinatario = "Peter Parker",
                    Messaggio = "Comprati un costume",
                    Importo = 10000,
                    DataDiScadenza = new DateTime(2022, 5, 15)
                });
            GiftCards.Add(
                new GiftCard
                {
                    Id = 2,
                    Mittente = "Paperon de Paperoni",
                    Destinatario = "Paolino Paperino",
                    Messaggio = "Buono per l'affitto",
                    Importo = 100,
                    DataDiScadenza = new DateTime(2022, 2, 1)
                });
        }
    }
}
