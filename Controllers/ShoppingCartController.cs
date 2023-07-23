using eTickets.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Data.Cart;
using Movie.Data.ViewModels;

namespace Movie.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart cart;
        private readonly IMoviesService moviesService;

        public ShoppingCartController(ShoppingCart cart,IMoviesService moviesService)
        {
            this.cart = cart;
            this.moviesService = moviesService;
        }
        // GET: ShoppingCartController
        public ActionResult Index()
        {
            var items = cart.GetShoppingCartItems();
            cart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartVM
            {
                ShoppingCart = cart,
                ShoppingCartTotal = cart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);

        }
        public async Task<RedirectToActionResult> AddToShoppingCart(int id)
        {
            var selectedmovie = await moviesService.GetMovieAsync(id);

            if (selectedmovie != null)
            {
                cart.AddItemToCart(selectedmovie);
            }
            return RedirectToAction("Index");
        }
        public async Task< RedirectToActionResult> RemoveFromShoppingCart(int id)
        {
            var selectedmovie = await moviesService.GetMovieAsync(id);

            if (selectedmovie != null)
            {
                cart.RemoveItemFromCart(selectedmovie);
            }
            return RedirectToAction("Index");
        }

    }
}
