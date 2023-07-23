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
    public class ActorsController : Controller
    {
        private readonly IActorsService actorsService;

        public ActorsController(IActorsService actorsService)
        {
            this.actorsService = actorsService;
        }
        // GET: Actors
        public async Task<IActionResult> Index()
        {
              return await actorsService.GetActorsAsync() != null ? 
                          View(await actorsService.GetActorsAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Actors'  is null.");
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if ( await actorsService.GetActorsAsync() == null)
            {
                return NotFound();
            }

            var actor = await actorsService.GetActorAsync(id);
                
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProfilePictureURL,FullName,Bio")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                await actorsService.AddActorAsync(actor);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if ( await actorsService.GetActorsAsync() == null)
            {
                return NotFound();
            }

            var actor = await actorsService.GetActorAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Actor actor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await actorsService.UpdateActorAsync(id,actor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await actorsService.GetActorAsync(id)==null)
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
            return View(actor);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (await actorsService.GetActorsAsync() == null)
            {
                return NotFound();
            }

            var actor = await actorsService.GetActorAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await actorsService.GetActorsAsync() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Actors'  is null.");
            }
            var actor = await actorsService.GetActorAsync(id);
            if (actor != null)
            {
                await actorsService.RemoveActorAsync(id);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

       
    }
}
