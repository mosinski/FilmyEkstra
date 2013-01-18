using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{ 
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        //
        // GET: /Movies/

        public ViewResult Index()
        {
            return View(db.Movies.ToList());
        }

        //
        // GET: /Movies/Details/5

        public ViewResult Details(int id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Movie movie = db.Movies.Find(id);
                return View(movie);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Movies/Create

        public ActionResult Create()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogOn", "Account");
            }
        } 

        //
        // POST: /Movies/Create

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    movie.Useradd = System.Web.HttpContext.Current.User.Identity.Name;
                    db.Movies.Add(movie);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(movie);
            }
            else
            {
                return RedirectToAction("LogOn", "Account");
            }

        }
        
        //
        // GET: /Movies/Edit/5

        public ActionResult Edit(int id)
        {
            Movie movie = db.Movies.Find(id);
            return View(movie);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie.Useradd = System.Web.HttpContext.Current.User.Identity.Name;
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Movies");
            }
            return View(movie);
        }

        //
        // GET: /Movies/Delete/5
 
        public ActionResult Delete(int id)
        {
            Movie movie = db.Movies.Find(id);
            return View(movie);
        }

        //
        // POST: /Movies/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (System.Web.HttpContext.Current.User.Identity.Name == movie.Useradd)
            {
                db.Movies.Remove(movie);
                db.SaveChanges();
                return RedirectToAction("Index", "Movies");
            }
            else
            {
                return RedirectToAction("Index", "Movies");
            }

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult SearchIndex(string searchString)
        {
            var movies = from m in db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            return View(movies);
        }
    }
}