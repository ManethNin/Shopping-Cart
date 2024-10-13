using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public int Id { get; set; }
        
        public bool IsDeleted { get; set; } = false;

        public ICollection<CartDetail> CartDetails { get; set; }
        
    }
}
