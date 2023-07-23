using Microsoft.AspNetCore.Identity;
using Movie.Data.Cart;
using Movie.Models;

namespace Movie.Data.Services
{
    public class OrderService : IOrderservice
    {
        private readonly ApplicationDbContext context;
        private readonly ShoppingCart cart;
      

        public OrderService(ApplicationDbContext context,ShoppingCart cart)
        {
            this.context = context;
            this.cart = cart;
            
        }
        public async Task StoreOrderAsync(Order order)
        {
            await context.AddAsync(order);
            await context.SaveChangesAsync();
           

            var items = cart.GetShoppingCartItems();
            foreach (var item in items)
            {
                var neworderitem = new OrderItem()
                {
                    MovieId=item.Id,
                    movie=item.Movie,
                    Order=order,
                    OrderId=order.Id
                };
                await context.OrderItems.AddAsync(neworderitem);
                await context.SaveChangesAsync();

            }

        }
    }
}
