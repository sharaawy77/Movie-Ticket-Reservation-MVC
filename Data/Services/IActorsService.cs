
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IActorsService
    {
        public  Task<IEnumerable<Actor>> GetActorsAsync();
        public Task<Actor> GetActorAsync(int id);
        public Task AddActorAsync(Actor actor);
        public Task RemoveActorAsync(int id);
        public Task UpdateActorAsync(int id,Actor actor);

    }
}
