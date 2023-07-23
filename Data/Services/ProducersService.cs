
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Movie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class ProducersService: IProducersService
    {
       private readonly ApplicationDbContext context;
        

       public ProducersService(ApplicationDbContext context)
       {
        this.context = context;
       }
    public async Task AddProducerAsync(Producer Producer)
    {

        await context.Producers.AddAsync(Producer);
        context.SaveChanges();


    }

    public async Task<Producer> GetProducerAsync(int id)
    {
        var Producer = await context.Producers.FindAsync(id);

        return Producer;

    }

    public async Task<IEnumerable<Producer>> GetProducersAsync()
    {
        var Producers = await context.Producers.ToListAsync();

        return Producers;

    }

    public async Task RemoveProducerAsync(int id)
    {
            context.Producers.Remove(await context.Producers.FindAsync(id));
    }

    public async Task UpdateProducerAsync(int id, Producer Producer)
    {
        context.Entry(Producer).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}
}
