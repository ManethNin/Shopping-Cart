using System.Diagnostics;
using BookShop.Data;
using BookShop.Models;
using BookShop.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index(string sterm = "", int genreId = 0)
    {
        var books = _context.Books.ToList();
        BookDisplayModel bookModel = new BookDisplayModel { Books = books, };
        
        return View(bookModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
