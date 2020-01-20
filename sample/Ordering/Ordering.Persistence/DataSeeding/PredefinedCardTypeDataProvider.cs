using System.Collections.Generic;
using BareBones.Domain.Enumerations;
using BareBones.Persistence.EntityFramework.Migration;
using Ordering.Domain.Models.BuyerAggregate;

namespace Ordering.Persistence.DataSeeding
{
    public class PredefinedCardTypeDataProvider :
        IDbDataProvider<IEnumerable<CardType>>
    {
        public IEnumerable<CardType> GetData()
        {
            return Enumeration.GetAll<CardType>();
        }
    }
}
