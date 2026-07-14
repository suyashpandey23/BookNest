using BookNest.Models.ViewModels;

namespace BookNest.BAL.Services.IServices;

public interface IShoppingCartService {
    
    Task<IEnumerable<ShoppingCart>> GetUserCartItemsAsync(string userId);
    Task<int> GetCartCountAsync(string userId);
    Task<ShoppingCart> AddToCartAsync(string userId);
    Task UpdateCartAsync(ShoppingCart cartItem);
    Task clearCartAsync(string userId);
}