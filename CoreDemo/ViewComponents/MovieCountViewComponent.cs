using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents
{
    public class MovieCountViewComponent:ViewComponent
    {
        private readonly Services.IMovieService _movieService;
        public MovieCountViewComponent(Services.IMovieService movieService)
        {
            this._movieService = movieService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int cinemaId)
        {
            var movies = await this._movieService.GetByCinemaAsync(cinemaId);
            var count = movies.Count();
            return this.View(count);
        }
    }
}
