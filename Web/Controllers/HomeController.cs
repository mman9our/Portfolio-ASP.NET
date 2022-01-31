using Core_Portfolio.Entities;
using Core_Portfolio.Interface;
using Infrastructure_Portfolio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Personal> _personal;
        private readonly IUnitOfWork<PortfolioItem> _portfolio;
        private DataContext _Context { get; set; }

        public HomeController(IUnitOfWork<Personal> personal, IUnitOfWork<PortfolioItem> Portfolio, DataContext Context)
        {
            _personal = personal;
            _portfolio = Portfolio;
             _Context= _Context;
        }
        public IActionResult Index()
        {
            var HomeViewModel = new HomeViewModel
            {
                PortfolioItem = _portfolio.Entity.GetAll().ToList()
            };
        return View(HomeViewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
