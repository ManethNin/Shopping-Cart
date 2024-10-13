using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;

        public CartRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<int> AddItem(int bookId, int qty)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                var cart = await GetCart();
                // Console.WriteLine(cart);
                // Console.WriteLine("\n\n\n\n\n\n\n\n");
                if (cart is null)
                {
                    Console.WriteLine("CART");
                    cart = new ShoppingCart();
                    _db.ShoppingCarts.Add(cart);
                }
                _db.SaveChanges();

                // cart detail section
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var book = _db.Books.Find(bookId);
                    cartItem = new CartDetail
                    {
                        BookId = bookId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        UnitPrice = book.Price  // it is a new line after update
                    };
                    _db.CartDetails.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var cartItemCount = await GetCartItemCount();
            return cartItemCount;
        }


        public async Task<int> RemoveItem(int bookId)
        {
            try
            {
                var cart = await GetCart();
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);
                if (cartItem is null)
                    throw new InvalidOperationException("Not items in cart");
                else if (cartItem.Quantity == 1)
                    _db.CartDetails.Remove(cartItem);
                else
                    cartItem.Quantity = cartItem.Quantity - 1;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            var cartItemCount = await GetCartItemCount();
            return cartItemCount;
        }

        public async Task<ShoppingCart> GetFullCart()
        {
            var shoppingCart = await _db.ShoppingCarts
                                  .Include(a => a.CartDetails)
                                  .ThenInclude(a => a.Book)
                                  .Include(a => a.CartDetails)
                                  .ThenInclude(a => a.Book)
                                  .FirstOrDefaultAsync();
            return shoppingCart;

        }
        public async Task<ShoppingCart> GetCart()
        {
            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.IsDeleted == false);
            return cart;
        }

        public async Task<int> GetCartItemCount()
        {
            var data = await (from cart in _db.ShoppingCarts
                              join cartDetail in _db.CartDetails
                              on cart.Id equals cartDetail.ShoppingCartId
                              select new { cartDetail.Id }
                        ).ToListAsync();
            return data.Count;
        }
    }
}
