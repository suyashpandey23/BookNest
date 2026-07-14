using BookNest.BAL.Services.IServices;
using BookNest.DAL.Data;
using BookNest.Models;
using Microsoft.EntityFrameworkCore;

namespace BookNest.BAL.Services;
public class ProductService : IProductService{
    
    private readonly ApplicationDbContext _context;
    
    public ProductService(ApplicationDbContext db) {
        _context = db;
    }

    
    public async Task<Product?> GetProductByIdAsync(int id, bool includeCategory = false) {
        
        if (includeCategory) {
            return await _context.Products.Include(u=>u.Category).FirstOrDefaultAsync(u=>u.Id==id);
        }
        else {
            return await _context.Products.FirstOrDefaultAsync(u => u.Id == id);
        }
        
        
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(bool includeCategory = false) {
        if (includeCategory) {
            return await _context.Products
                .Include(c => c.Category)
                .ToListAsync();
        }
        else {
            return await _context.Products.ToListAsync();
        }

    }

    public async Task<Product> CreateProductAsync(Product product){
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
    

    public async Task UpdateProductAsync(Product product) {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id) {
        var product=_context.Products.FirstOrDefault(c=>c.Id==id);
        if (product == null) {
            throw new KeyNotFoundException($"Product with id {id} not found");
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
    
}