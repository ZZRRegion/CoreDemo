using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemo.Model;

namespace CoreDemo.Services
{
    public class MovieMemoryService : IMovieService
    {
        private readonly List<Movie> _movies = new List<Movie>();
        public MovieMemoryService()
        {
            this._movies.Add(new Movie()
            {
                CinemaId = 1,
                Id = 1,
                Name = "Superman",
                ReleaseDate = new DateTime(2018, 10, 1),
                Starring = "Nick"
            });
            this._movies.Add(new Movie()
            {
                CinemaId = 1,
                Id = 2,
                Name = "Ghost",
                ReleaseDate = new DateTime(1997, 5, 3),
                Starring = "Michael Jackson"
            });
        }
        public Task AddAsync(Movie movie)
        {
            if (this._movies.Count > 0)
            {
                int maxId = this._movies.Max(x => x.Id);
                movie.Id = maxId + 1;
            }
            else
            {
                movie.Id = 1;
            }
            this._movies.Add(movie);
            return Task.CompletedTask;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            Movie movie = await this.GetByIdAsync(id);
            return await Task.Run(() => this._movies.Remove(movie));
        }

        public Task<IEnumerable<Movie>> GetByCinemaAsync(int cinemaId)
        {
            return Task.Run(() => this._movies.Where(x => x.CinemaId == cinemaId));
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await Task.Run(() => this._movies.FirstOrDefault(x => x.Id == id));
        }
    }
}
