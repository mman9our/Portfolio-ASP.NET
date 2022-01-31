using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core_Portfolio.Entities;
using Infrastructure_Portfolio;
using Web.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Core_Portfolio.Interface;
using Microsoft.AspNetCore.Http;

namespace Web.Controllers
{
    public class PortfolioItemsController : Controller
    {
        private readonly IUnitOfWork<PortfolioItem> _portfolio;
        private readonly IHostingEnvironment _hosting;
        private DataContext _Context { get; set; }

        public PortfolioItemsController(IUnitOfWork<PortfolioItem> portfolio, IHostingEnvironment hosting, DataContext _Context)
        {
            _portfolio = portfolio;
            _hosting = hosting;
            this._Context = _Context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(ULogin log)
        {

            
            Personal emp = _Context.Personal.FirstOrDefault(a=>a.Profile.Equals(log.Email) && a.Avatar.Equals(log.Password));

            if (emp != null)
            {
                HttpContext.Session.SetString("email", emp.Profile);
                HttpContext.Session.SetString("name", emp.FullName);
                return RedirectToAction("Index","Home");
            }
            ViewBag.Error = "Invalid Email / Password";
            return View(log);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }




        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("name") == null)
                return RedirectToAction("Login");

            return View(_portfolio.Entity.GetAll());
        }

        [HttpPost]
        public IActionResult Index(string SearchKey)
        {
            if(HttpContext.Session.GetString("name") == null)
                return RedirectToAction("Login");

            var items = _Context.PortfolioItem.Where(a => a.ProjectName.Contains(SearchKey)).ToList();
            
            return View(items);
        }

        //___________________Start Main functions______________________//

        [HttpGet]
        public IActionResult Details(Guid? id)
        {
            if (HttpContext.Session.GetString("name") == null)
                return RedirectToAction("Login");

          

            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("name") == null)
                return RedirectToAction("Login");
            if (!HttpContext.Session.GetString("email").Equals("moh@gmail.com"))
                return RedirectToAction("Login");

            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioViewModel model)
        {
            if (HttpContext.Session.GetString("name") == null)
                return RedirectToAction("Login");

            if (!HttpContext.Session.GetString("email").Equals("moh@gmail.com"))
                return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                PortfolioItem portfolioItem = new PortfolioItem
                {
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };

                _portfolio.Entity.Insert(portfolioItem);
               
                _portfolio.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            if (HttpContext.Session.GetString("name") == null)
                return RedirectToAction("Login");

            if (!HttpContext.Session.GetString("email").Equals("moh@gmail.com"))
                return RedirectToAction("Login");

            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            PortfolioViewModel portfolioViewModel = new PortfolioViewModel
            {
                Id = portfolioItem.Id,
                Description = portfolioItem.Description,
                ImageUrl = portfolioItem.ImageUrl,
                ProjectName = portfolioItem.ProjectName
            };

            return View(portfolioViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortfolioViewModel model)
        {
            if (HttpContext.Session.GetString("name") == null)
                return RedirectToAction("Login");

            if (!HttpContext.Session.GetString("email").Equals("moh@gmail.com"))
                return RedirectToAction("Login");

            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string fullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    PortfolioItem portfolioItem = new PortfolioItem
                    {

                        Id = model.Id,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName
                    };

                    _portfolio.Entity.Update(portfolioItem);
                    _portfolio.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.Id))
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
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid? id)
        {
            if (HttpContext.Session.GetString("name") == null)
                return RedirectToAction("Login");

            if (!HttpContext.Session.GetString("email").Equals("moh@gmail.com"))
                return RedirectToAction("Login");

            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (HttpContext.Session.GetString("name") == null)
                return RedirectToAction("Login");

            if (!HttpContext.Session.GetString("email").Equals("moh@gmail.com"))
                return RedirectToAction("Login");

            _portfolio.Entity.Delete(id);
            _portfolio.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(Guid id)
        {

            return _portfolio.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
