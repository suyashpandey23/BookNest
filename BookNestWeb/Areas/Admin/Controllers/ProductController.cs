using BookNest.BAL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using BookNest.DAL.Data;
using BookNest.Models;
using BookNest.Models.ViewModels;
using BookNest.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookNest.Areas.Customer.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.RoleAdmin)]
public class ProductController : Controller {
    // GET
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IProductService productService,ICategoryService categoryService,IWebHostEnvironment webHostEnvironment) {
        _productService = productService;
        _categoryService = categoryService;
        _webHostEnvironment = webHostEnvironment;
    }
    
    [AllowAnonymous]
    public async Task<IActionResult> Index() {
        return View();
    }
    

    public async Task<IActionResult> Upsert(int? id) {

        var categories = await _categoryService.GetAllCategoriesAsync();

        ProductVM ProductVM = new()
        { CategoryList = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }),
            Product = new Product()
        };
        if (id == null || id == 0) {
            //we are creating
            return View(ProductVM);
        }
        else {
            //we are updating/editing
            ProductVM.Product = await _productService.GetProductByIdAsync(id.Value);
            return View(ProductVM);
        }
        // IEnumerable<SelectListItem> categories = (await _categoryService.GetAllCategoriesAsync()).Select(c=>new SelectListItem{
        //     Text = c.Name,
        //     Value = c.Id.ToString()
        // });
        //
        // ViewBag.CategoryList=categories;
        // return View(ProductVM);
    }
    
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Upsert")]
    public async Task<IActionResult> UpsertPOST(ProductVM productVM,IFormFile? file) {

        
        if (ModelState.IsValid) {
            
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null) {
                string fileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                string productpath = Path.Combine("images","products");
                string finalPath= Path.Combine(wwwRootPath,productpath);

                if (!Directory.Exists(finalPath)) {
                    Directory.CreateDirectory(finalPath);
                }

                using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create)) {
                    file.CopyTo(fileStream);
                }
                productVM.Product.ImageUrl = "~/" + Path.Combine(productpath, fileName).Replace("\\", "/");
                //productVM.Product.ImageUrl=Path.Combine(@"\",productpath,fileName).Replace("\\","/");
            }  
            //await _productService.CreateProductAsync(productVM.Product);
            
            if (productVM.Product.Id == null || productVM.Product.Id == 0) {
                //create
                await _productService.CreateProductAsync(productVM.Product);
                TempData["success"] = "Product created successfully";
            }
            else {
                await _productService.UpdateProductAsync(productVM.Product);
                TempData["success"] = "Product Updated successfully";
            }
            
            return RedirectToAction("Index");
        }
        else {
            var categories= await _categoryService.GetAllCategoriesAsync();
            productVM.CategoryList = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(productVM);
        }
    }
    
    
    [HttpDelete]
    public async Task<IActionResult> Delete(int? id) {

        if (id == null || id == 0)
        {
            return Json(new { success = false, message = "Invalid ID" });
        }

        var productToBeDeleted = await _productService.GetProductByIdAsync(id.Value);
        if (productToBeDeleted == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        //delete product image if that exists
        if (!string.IsNullOrEmpty(productToBeDeleted.ImageUrl))
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('~', '/'));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }


        await _productService.DeleteProductAsync(id.Value);
        return Json(new { success = true, message = "Delete Successful" });
    }
    
    // [HttpDelete]
    // public async Task<IActionResult> DeletePOST(int id) {
    //     if (id == null || id == 0) {
    //         return Json(new { success = false, message = "Invalid id" });
    //     } 
    //     
    //     var productTobeDeleted=await _productService.GetProductByIdAsync(id.Value);
    //     if (productTobeDeleted == null) {
    //         return Json(new { success = false, message = "Product not found" });
    //     }
    //     //if found , delete the image related to the product as well
    //     if (!string.IsNullOrEmpty(productTobeDeleted.ImageUrl)) {
    //         var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, productTobeDeleted.ImageUrl.TrimStart('~', '/'));
    //         if (System.IO.File.Exists(imagePath)) {
    //             System.IO.File.Delete(imagePath);
    //         }
    //     }
    //     await _productService.DeleteProductAsync(id); 
    //    TempData["success"] = "Product deleted successfully";
    //    return RedirectToAction("Index");
    //    
    // }
    
    #region API CALLS
    [AllowAnonymous]
    public async Task<IActionResult> GetAll() {
        var products = await _productService.GetAllProductsAsync(true);
         return Json(new { data = products });
    }
    #endregion
}