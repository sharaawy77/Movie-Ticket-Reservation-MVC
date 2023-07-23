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
    public class MoviesController : Controller
    {
        
        private readonly IMoviesService moviesService;
        private readonly ICinemasService cinemasService;
        private readonly IProducersService producersService;

        public MoviesController(IMoviesService moviesService,ICinemasService cinemasService,IProducersService producersService)
        {
            
            this.moviesService = moviesService;
            this.cinemasService = cinemasService;
            this.producersService = producersService;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await moviesService.GetMoviesAsync();
            return View( applicationDbContext);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (await moviesService.GetMoviesAsync() == null)
            {
                return NotFound();
            }

            var movie = await moviesService.GetMovieAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public async Task< IActionResult> Create()
        {
            ViewData["CinemaId"] = new SelectList(await cinemasService.GetCinemasAsync() , "Id", "Name");
            ViewData["ProducerId"] = new SelectList(await producersService.GetProducersAsync(), "Id", "FullName");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ImageURL,StartDate,EndDate,MovieCategory,CinemaId,ProducerId")] eTickets.Models.Movie movie)
        {
            if (ModelState.IsValid)
            {
                await moviesService.AddMovieAsync(movie);
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaId"] = new SelectList(await cinemasService.GetCinemasAsync(), "Id", "Description", movie.CinemaId);
            ViewData["ProducerId"] = new SelectList(await producersService.GetProducersAsync(), "Id", "Bio", movie.ProducerId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (await moviesService.GetMoviesAsync() == null)
            {
                return NotFound();
            }

            var movie = await moviesService.GetMovieAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["CinemaId"] = new SelectList(await cinemasService.GetCinemasAsync(), "Id", "Name", movie.CinemaId);
            ViewData["ProducerId"] = new SelectList(await producersService.GetProducersAsync(), "Id", "FullName", movie.ProducerId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ImageURL,StartDate,EndDate,MovieCategory,CinemaId,ProducerId")] eTickets.Models.Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await moviesService.UpdateMovieAsync(id, movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await moviesService.GetMovieAsync(id)==null)
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
            ViewData["CinemaId"] = new SelectList(await cinemasService.GetCinemasAsync(), "Id", "Description", movie.CinemaId);
            ViewData["ProducerId"] = new SelectList(await producersService.GetProducersAsync(), "Id", "Bio", movie.ProducerId);
            return View(movie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Filter(string MovieName)
        {
            if (MovieName != null)
            {
                var result = await moviesService.Filter(MovieName);
                if (result.Id == -1)
                {
                    return View();
                }
                return RedirectToAction(nameof(Details),new {Id=result.Id});
            }
            return View("Enter Movie Name") ;
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (await moviesService.GetMoviesAsync() == null)
            {
                return NotFound();
            }

            var movie = await moviesService.GetMovieAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await moviesService.GetMoviesAsync() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
            }
            var movie = await moviesService.GetMovieAsync(id);
            if (movie != null)
            {
                await moviesService.RemoveMovieAsync(id);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        //private bool MovieExists(int id)
        //{
        //  return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
