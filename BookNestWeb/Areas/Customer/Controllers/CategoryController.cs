using BookNest.BAL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using BookNest.Data;
using BookNest.Models;

namespace BookNest.Areas.Customer.Controllers;

[Area("Customer")]
public class CategoryController : Controller {
    // GET
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService) {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index() {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return View(categories);
    }

    public IActionResult Create() {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Create")]
    public async Task<IActionResult> CreatePOST(Category category) {

        //match if same name is already present
        if (!string.IsNullOrEmpty(category.Name) && !await _categoryService.IsCategoryNameUniqueAsync(category.Name)) {
            ModelState.AddModelError("Name", "Category name already exists");
        }

        if (ModelState.IsValid) {
            await _categoryService.CreateCategoryAsync(category);
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        return View();

    }

    public async Task<IActionResult> Update(int? id) {

        if (id == null || id == 0) {
            return NotFound();
        }

        var category = await _categoryService.GetCategoryByIdAsync(id.Value);
        if (category == null) {
            return NotFound();
        }

        return View(category);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Update")]
    public async Task<IActionResult> UpdatePOST(Category category) {
        if( !string.IsNullOrEmpty(category.Name) && !await _categoryService.IsCategoryNameUniqueAsync(category.Name, category.Id) ) 
        {
            ModelState.AddModelError("Name", "Category name already exists");
        }
        if (ModelState.IsValid) {
            await _categoryService.UpdateCategoryAsync(category);
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        return View();
    }
    
    
    public async Task<IActionResult> Delete(int? id) {

        if (id == null || id == 0) {
            return NotFound();
        }

        var category = await _categoryService.GetCategoryByIdAsync(id.Value);
        if (category == null) {
            return NotFound();
        }
        
        return View(category);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePOST(int id) {
       await _categoryService.DeleteCategoryAsync(id); 
       TempData["success"] = "Category deleted successfully";
       return RedirectToAction("Index");
       
    }
    
}