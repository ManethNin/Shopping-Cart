using BookShop.Models;

namespace BookShop.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int bookId, int qty);
        Task<int> RemoveItem(int bookId);
        Task<ShoppingCart> GetFullCart();
        Task<int> GetCartItemCount();
        Task<ShoppingCart> GetCart();
    }
}
