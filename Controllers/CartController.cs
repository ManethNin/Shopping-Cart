using BookShop.Data;
using BookShop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICartRepository _cartRepo;

        public CartController(ILogger<CartController> logger, ICartRepository cartRepo)
        {
            _logger = logger;
            _cartRepo = cartRepo;
        }

        public async Task<IActionResult> AddItem(int bookId, int qty = 1, int redirect = 0)
        {
            _logger.LogInformation("ASFKASDFALSKDFJALSKDFJALSDKjfLSJkA\n\n\n\n\n");
            var cartCount = await _cartRepo.AddItem(bookId, qty);

            if (redirect == 0)
                return Ok(cartCount);

            return RedirectToAction("GetFullCart");
        }

        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var cartCount = await _cartRepo.RemoveItem(bookId);
            return RedirectToAction("GetFullCart");
        }

        public async Task<IActionResult> GetFullCart()
        {
            var cart = await _cartRepo.GetFullCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }

        public IActionResult Checkout()
        {
            return View();
        }

    }
}
