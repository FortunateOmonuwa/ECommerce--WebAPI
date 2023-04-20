using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServiceContracts
{
    public interface ICartService
    {
        Task<Cart> CreateCart(Cart cart, int customerId);
        Task<Cart> UpdateCart(Cart cart);
        Task<bool> DeleteCart(int cartId);
        Task<Cart> GetCartById(int cartId, int cusotmerId);
        Task<Product> AddProductToCart(Product product, int cartId, int customerId);
        Task<bool> RemoveProductFromCart(int cartId, int productId);
        Task<bool> RemoveAllProductsFromCart(int cartId, int customerId);
        Task<ICollection<Product>> GetAllCartProducts(int customerId, int cartId);
        Task<int> GetProductCount(int customerId, int cartId);
        Task<Product> GetCartProductById(int productId, int cardId, int customerId);
        Task<int> GetTotalPrice(int customerId, int cartId);
    }
}
