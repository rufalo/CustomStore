using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomStoreApi.Context.Tabels
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser_Id")]
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUser_Id { get; set; }

        [ForeignKey("Offer_Id")]
        public Offer Offer { get; set; }
        public int Offer_Id { get; set; }

        public int Status { get; set; }
    }
}
