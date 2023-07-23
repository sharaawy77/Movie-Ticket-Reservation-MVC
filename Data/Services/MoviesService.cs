
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Movie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MoviesService :IMoviesService
    {
        private readonly ApplicationDbContext context;

        public MoviesService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddMovieAsync(eTickets.Models.Movie Movie)
        {

            await context.Movies.AddAsync(Movie);
            context.SaveChanges();


        }
        public async  Task<eTickets.Models.Movie> Filter(string MovieName)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Name == MovieName);
            if (movie == null) { return new Models.Movie() { Id = -1 }; }
            return movie;
        }

        public async Task<eTickets.Models.Movie> GetMovieAsync(int id)
        {
            var Movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            return Movie;

        }
        public async Task<IEnumerable<eTickets.Models.Movie>> GetMoviesAsync()
        {
            var Movies = await context.Movies.ToListAsync();

            return Movies;

        }

        public async Task RemoveMovieAsync(int id)
        {
            context.Remove(await context.Movies.FindAsync(id));
        }

        public async Task UpdateMovieAsync(int id, eTickets.Models.Movie Movie)
        {
            context.Entry(Movie).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
