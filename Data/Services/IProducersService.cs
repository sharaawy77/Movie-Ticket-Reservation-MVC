
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IProducersService
    {
        public Task<IEnumerable<Producer>> GetProducersAsync();
        public Task<Producer> GetProducerAsync(int id);
        public Task AddProducerAsync(Producer Producer);
        public Task RemoveProducerAsync(int id);
        public Task UpdateProducerAsync(int id, Producer Producer);

    }
}
