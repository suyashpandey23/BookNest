using BookNest.BAL.Services.IServices;
using BookNest.Models.ViewModels;

namespace BookNest.BAL.Services;

public class ShoppingCartService : IShoppingCartService{
    public Task<IEnumerable<ShoppingCart>> GetUserCartItemsAsync(string userId) {
        throw new NotImplementedException();
    }

    Task<int> IShoppingCartService.GetCartCountAsync(string userId) {
        throw new NotImplementedException();
    }

    public Task<ShoppingCart> AddToCartAsync(string userId) {
        throw new NotImplementedException();
    }

    public Task UpdateCartAsync(ShoppingCart cartItem) {
        throw new NotImplementedException();
    }

    public Task clearCartAsync(string userId) {
        throw new NotImplementedException();
    }
    
}