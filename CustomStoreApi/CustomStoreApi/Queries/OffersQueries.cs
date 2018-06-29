using CustomStoreApi.Context;
using CustomStoreApi.Context.Tabels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomStoreApi.Queries
{
    public class OffersQueries
    {
        private readonly ApplicationContext _Context;
              

        public OffersQueries(ApplicationContext context)
        {
            _Context = context;
        }

        public IEnumerable<Offer> GetAllVisableOffers()
        {
            return _Context.Offers.Where(x => x.Visable == true);
        }

        public IEnumerable<Offer> GetAllOffers()
        {
            return _Context.Offers;
        }

        public Offer NewOffer(Offer offer)
        {
            _Context.Offers.Add(offer);
            _Context.SaveChanges();
            return offer;
        }

        public Offer EditOffer(Offer offer)
        {
            var toEdit = _Context.Offers.SingleOrDefault(x => x.Id == offer.Id);
            if (toEdit != null)
            {
                toEdit.MainImage = offer.MainImage;
                toEdit.Name = offer.Name;
                toEdit.Price = offer.Price;
                toEdit.ShortDescription = offer.ShortDescription;
                toEdit.Visable = offer.Visable;
                _Context.SaveChanges();
            }

            return toEdit;
        }

        public bool DeleteOffer(Offer offer)
        {
            var found = _Context.Offers.SingleOrDefault(x => x.Id == offer.Id);
            if (found == null)
                return false;
            _Context.Offers.Remove(found);
            _Context.SaveChanges();
            return true;
        }

    }
}
