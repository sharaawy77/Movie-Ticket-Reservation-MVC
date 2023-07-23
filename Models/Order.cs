using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; } = "";
        [Required]
        public string CustomerEmail { get; set; } = "";
        [Required]
        public string Address { get; set; } = "";

        public DateTime orderdate { get; set; } = DateTime.Now;
        public string UserId { get; set; } = "";
        [ForeignKey(nameof(UserId))]
        public virtual IdentityUser User { get; set; } = new IdentityUser();
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
