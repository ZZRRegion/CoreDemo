using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemo.Services;
using CoreDemo.Model;

namespace CoreDemo.Controllers
{
    public class MovieController:Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICinemaService _cinemaService;
        public MovieController(IMovieService movieService,
            ICinemaService cinemaService)
        {
            this._movieService = movieService;
            this._cinemaService = cinemaService;
        }
        public async Task<IActionResult> Index(int cinemaId)
        {
            Cinema cinema = await this._cinemaService.GetByIdAsync(cinemaId);
            this.ViewBag.Title = $"{cinema.Name}这个电影院上映的电影有：";
            this.ViewBag.CinemaId = cinemaId;
            return this.View(await this._movieService.GetByCinemaAsync(cinemaId));
        }
        public IActionResult Add(int cinemaId)
        {
            this.ViewBag.Title = "添加电影";
            return this.View(new Movie() { CinemaId = cinemaId });
        }
        [HttpPost]
        public async Task<IActionResult> Add(Movie movie)
        {
            if (this.ModelState.IsValid)
            {
                await this._movieService.AddAsync(movie);
            }
            return this.RedirectToAction("Index", new { cinemaId = movie.CinemaId });
        }
        public IActionResult Edit(int movieId)
        {

            return this.RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int movieId)
        {
            Movie movie = await this._movieService.GetByIdAsync(movieId);

            bool b = await this._movieService.DeleteByIdAsync(movieId);
            return this.RedirectToAction("Index", new { cinemaId = movie.CinemaId });
        }
    }
}
