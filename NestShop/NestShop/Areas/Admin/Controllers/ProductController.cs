using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestShop.Data;
using NestShop.Models;

namespace NestShop.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        List<Product> products = await _context.Products
                                                .Include(x => x.ProductImages)
                                                .Include(x => x.Category)
                                                .ToListAsync();
        return View(products);
    }
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        await _context.AddAsync(product);

         return RedirectToAction("Index");
    }
    public async Task<IActionResult> Detail(int? id)
    {
        if (id == null || id <= 0) return BadRequest();
        var product = await _context.Products.Include(x => x.Category)
                                             .Include(x => x.ProductImages)
                                             .Include(nameof(ProductSize.Size))
                                             .FirstOrDefaultAsync(x => x.Id == id);
        if (product == null) return NotFound();
        return View(product);
    }
}
