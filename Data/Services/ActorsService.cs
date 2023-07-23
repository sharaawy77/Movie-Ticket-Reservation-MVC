
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Movie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly ApplicationDbContext context;

        public ActorsService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddActorAsync(Actor actor)
        {
           
                await context.Actors.AddAsync(actor);
                context.SaveChanges();
         

        }

        public async Task<Actor> GetActorAsync(int id)
        {
            var actor = await context.Actors.FindAsync(id);
           
            return actor;
           
        }

        public async Task<IEnumerable<Actor>> GetActorsAsync()
        {
            var Actors = await context.Actors.ToListAsync();
           
            return Actors;
           
        }

        public async Task RemoveActorAsync(int id)
        {
            var actor =await context.Actors.FindAsync(id);
            context.Actors.Remove(actor);
        }

        public async Task UpdateActorAsync(int id, Actor actor)
        {
            context.Entry(actor).State= EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
