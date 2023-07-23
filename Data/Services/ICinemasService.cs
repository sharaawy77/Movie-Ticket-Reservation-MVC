
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface ICinemasService
    {
        public Task<IEnumerable<Cinema>> GetCinemasAsync();
        public Task<Cinema> GetCinemaAsync(int id);
        public Task AddCinemaAsync(Cinema Cinema);
        public Task RemoveCinemaAsync(int id);
        public Task UpdateCinemaAsync(int id, Cinema cinema);
    }
}
