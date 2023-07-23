using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie.Data;
using eTickets.Models;
using eTickets.Data.Services;

namespace Movie.Controllers
{
    public class ProducersController : Controller
    {
        
        private readonly IProducersService producersService;

        public ProducersController(IProducersService producersService )
        {
            
            this.producersService = producersService;
        }

        // GET: Producers
        public async Task<IActionResult> Index()
        {
              return await producersService.GetProducersAsync() != null ? 
                          View(await producersService.GetProducersAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Producers'  is null.");
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (await producersService.GetProducersAsync() == null)
            {
                return NotFound();
            }

            var producer = await producersService.GetProducerAsync(id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                await producersService.AddProducerAsync(producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (producersService.GetProducersAsync() == null)
            {
                return NotFound();
            }

            var producer = await producersService.GetProducerAsync(id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (id != producer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await producersService.UpdateProducerAsync(id, producer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await producersService.GetProducerAsync(id)==null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (await producersService.GetProducersAsync() == null)
            {
                return NotFound();
            }

            var producer = await producersService.GetProducerAsync(id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await producersService.GetProducersAsync() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Producers'  is null.");
            }
            var producer = await producersService.GetProducerAsync(id);
            if (producer != null)
            {
                await producersService.RemoveProducerAsync(id);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

       
    }
}
