using System.ComponentModel.DataAnnotations;

namespace Movie.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public eTickets.Models.Movie Movie { get; set; }
        public int Amount { get; set; }


        public string ShoppingCartId { get; set; }
    }
}
