using Movie.Models;

namespace Movie.Data.Services
{
    public interface IOrderservice
    {
        Task StoreOrderAsync(Order order);
    }
}
