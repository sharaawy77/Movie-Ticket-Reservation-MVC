using Microsoft.AspNetCore.Mvc;
using Movie.Data.Cart;
using Movie.Data.ViewModels;

namespace Movie.viewcomponent
{
    public class ShoppingCartViewComponent:ViewComponent
    {
        private readonly ShoppingCart cart;

        public ShoppingCartViewComponent(ShoppingCart cart)
        {
            this.cart = cart;
        }
        public IViewComponentResult Invoke()
        {
            var items=cart.GetShoppingCartItems();
            cart.ShoppingCartItems=items;
            var SCVM = new ShoppingCartVM()
            {
                ShoppingCart = cart,
                ShoppingCartTotal = cart.GetShoppingCartTotal()
            };
            return View(SCVM);
        }
    }
}
