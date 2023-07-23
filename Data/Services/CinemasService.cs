
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Movie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class CinemasService : ICinemasService
    {
        private readonly ApplicationDbContext context;

        public CinemasService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddCinemaAsync(Cinema cinema)
        {

            await context.Cinemas.AddAsync(cinema);
            context.SaveChanges();


        }

        public async Task<Cinema> GetCinemaAsync(int id)
        {
            var cinema = await context.Cinemas.FindAsync(id);

            return cinema;

        }
        public async Task<IEnumerable<Cinema>> GetCinemasAsync()
        {
            var Cinemas = await context.Cinemas.ToListAsync();

            return Cinemas;

        }

        public async Task RemoveCinemaAsync(int id)
        {
            context.Remove(await context.Cinemas.FindAsync(id));
        }

        public async Task UpdateCinemaAsync(int id, Cinema Cinema)
        {
            context.Entry(Cinema).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
