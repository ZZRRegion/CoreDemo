using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemo.Services;
using CoreDemo.Model;
using Microsoft.Extensions.Options;

namespace CoreDemo.Controllers
{
    public class HomeController:Controller
    {
        private readonly ICinemaService _cinemaService;
        public HomeController(ICinemaService cinemaService)
        {
            this._cinemaService = cinemaService;
        }
        public async Task<IActionResult> Index()
        {
            this.ViewBag.Title = "电影院列表";
            var obj = await this._cinemaService.GetAllAsync();
            return this.View(obj);
        }
        public IActionResult Add()
        {
            this.ViewBag.Title = "添加电影院";
            return this.View(new Cinema());
        }
        [HttpPost]
        public async Task<IActionResult> Add(Cinema cinema)
        {
            if (this.ModelState.IsValid)
            {
                await this._cinemaService.AddAsync(cinema);
            }
            return this.RedirectToAction("Index");
        }
        public IActionResult Delete(int cinemaId)
        {
            this._cinemaService.DeleteByIdAsync(cinemaId);
            return this.RedirectToAction("Index");
        }
        public IActionResult Edit(int cinemaId)
        {
            return this.RedirectToAction("Index");
        }
    }
}
