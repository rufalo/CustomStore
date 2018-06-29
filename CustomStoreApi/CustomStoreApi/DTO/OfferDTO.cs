using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomStoreApi.DTO
{
    public class OfferDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainImage { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int Price { get; set; }
    }
}
