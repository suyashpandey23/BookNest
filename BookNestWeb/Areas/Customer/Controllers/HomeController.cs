using System.Diagnostics;
using BookNest.BAL.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers;
[Area("Customer")]
public class HomeController : Controller {
    
    private readonly IProductService _productService;
    
    public HomeController(IProductService productService) {
        _productService = productService;
    }
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync(includeCategory: true);
        return View(products);
    }
    
    public async Task<IActionResult> Details(int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId, includeCategory: true);
        // if (product == null){
        //     return NotFound();
        // }
        // ShoppingCart cart = new(){
        //     Product = product,
        //     Count = 1,
        //     ProductId = productId
        // };
        return View(product);
    }
    
    public IActionResult Privacy() {
        return View();
    }

   
}