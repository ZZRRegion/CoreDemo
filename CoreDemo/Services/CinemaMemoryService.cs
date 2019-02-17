using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemo.Model;

namespace CoreDemo.Services
{
    public class CinemaMemoryService : ICinemaService
    {
        private readonly List<Cinema> _cinemas = new List<Cinema>();
        public CinemaMemoryService()
        {
            this._cinemas.Add(new Cinema()
            {
                Id = 1,
                Name = "City Cinema",
                Location = "Road ABC,No.123",
                Capacity = 1000
            });
            this._cinemas.Add(new Cinema()
            {
                Id = 2,
                Name = "Fly Cinema",
                Location = "Road Hello,No.1024",
                Capacity = 500
            });
        }
        public Task AddAsync(Cinema model)
        {
            int maxId = this._cinemas.Max(x => x.Id);
            model.Id = maxId + 1;
            this._cinemas.Add(model);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Cinema>> GetAllAsync()
        {
            return Task.Run(() => this._cinemas.AsEnumerable());
        }

        public Task<Cinema> GetByIdAsync(int id)
        {
            return Task.Run(() => this._cinemas.FirstOrDefault(x => x.Id == id));
        }
        public async Task<bool> DeleteByIdAsync(int id)
        {
            Cinema cinema = await this.GetByIdAsync(id);
            return await Task.Run<bool>(() => {
                this._cinemas.Remove(cinema);
                return true;
            });
        }
    }
}
