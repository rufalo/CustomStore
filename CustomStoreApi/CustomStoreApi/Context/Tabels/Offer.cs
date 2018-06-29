using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomStoreApi.Context.Tabels
{
    public class Offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string MainImage { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public bool Visable { get; set; }

    }
}
