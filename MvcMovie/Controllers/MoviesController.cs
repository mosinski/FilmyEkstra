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
            Movie movie = db.Movies.Find(id);
            return View(movie);
        }

        //
        // GET: /Movies/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Movies/Create

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                //movie.Useradd.Insert(1,System.Web.HttpContext.Current.User.Identity.Name);
                movie.Useradd = System.Web.HttpContext.Current.User.Identity.Name;
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(movie);
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
                return RedirectToAction("Index");
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
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
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