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
    public class CinemasController : Controller
    {
        private readonly ICinemasService cinemasService;

        public CinemasController(ICinemasService cinemasService)
        {
            this.cinemasService = cinemasService;
        }

        // GET: Cinemas
        public async Task<IActionResult> Index()
        {
              return await cinemasService.GetCinemasAsync() != null ? 
                          View(await cinemasService.GetCinemasAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Cinemas'  is null.");
        }

        // GET: Cinemas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (await cinemasService.GetCinemasAsync() == null)
            {
                return NotFound();
            }

            var cinema = await cinemasService.GetCinemaAsync(id);
                
            if (cinema == null)
            {
                return NotFound();
            }

            return View(cinema);
        }

        // GET: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cinemas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                await cinemasService.AddCinemaAsync(cinema);
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }

        // GET: Cinemas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (await cinemasService.GetCinemasAsync() == null)
            {
                return NotFound();
            }

            var cinema = await cinemasService.GetCinemaAsync(id);
            if (cinema == null)
            {
                return NotFound();
            }
            return View(cinema);
        }

        // POST: Cinemas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (id != cinema.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await cinemasService.UpdateCinemaAsync(id,cinema);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await cinemasService.GetCinemaAsync(id)==null)
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
            return View(cinema);
        }

        // GET: Cinemas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (await cinemasService.GetCinemasAsync() == null)
            {
                return NotFound();
            }

            var cinema = await cinemasService.GetCinemaAsync(id);
            if (cinema == null)
            {
                return NotFound();
            }

            return View(cinema);
        }

        // POST: Cinemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await cinemasService.GetCinemasAsync() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cinemas'  is null.");
            }
            var cinema = await cinemasService.GetCinemaAsync(id);
            if (cinema != null)
            {
                await cinemasService.RemoveCinemaAsync(id);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

       
    }
}
