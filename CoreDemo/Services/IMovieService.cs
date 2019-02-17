using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemo.Model;

namespace CoreDemo.Services
{
    public interface IMovieService
    {
        Task AddAsync(Movie movie);
        Task<Movie> GetByIdAsync(int id);
        Task<IEnumerable<Movie>> GetByCinemaAsync(int cinemaId);
        Task<bool> DeleteByIdAsync(int id);
    }
}
