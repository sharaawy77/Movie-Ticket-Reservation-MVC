
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IMoviesService
    {
        public Task<IEnumerable<eTickets.Models.Movie>> GetMoviesAsync();
        public Task<eTickets.Models.Movie> GetMovieAsync(int id);
        public Task AddMovieAsync(eTickets.Models.Movie Movie);
        public Task RemoveMovieAsync(int id);
        public Task UpdateMovieAsync(int id, eTickets.Models.Movie Movie);
        public  Task<eTickets.Models.Movie> Filter(string MovieName);
    }
}
