using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.RandomMovieViewModel;
using System.Data.Entity;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class MoviesController : Controller
    {
        public ApplicationDbContext _context;

        public MoviesController()
        {
           _context = new ApplicationDbContext();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movw = new List<Movie>
            {
                new Movie {Name="Shrek" }

            };

            var customerList = new List<Customer>
            {
                new Customer {Name="Alin" },
                new Customer {Name="Sergiu" }
            };

            var movieCustomer = new RandomMovieCustomer
            {
                movie = movw,
                customers = customerList
            };
              return View(movieCustomer);
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
        }

        //public ActionResult Edit(int id)
        //{
        //    return Content("id=" + id);
        //}

        //public ActionResult Index(int? pageNumber, string sortBy)
        //{
        //    if (!pageNumber.HasValue)
        //        pageNumber = 1;
        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";

        //    return Content(String.Format("Page numer={0} SortBy={1}", pageNumber, sortBy));
        //}
        [Route("movies/released/{year}/{month:regex(\\d{2})}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult Customers()
        {

            var custom = new List<Customer>
            {
                new Customer {Name="Ionut Popescu", Id=1 },
                new Customer {Name="Mitran Marian", Id=2 }
            };

            var custMovie = new RandomMovieCustomer
            {
                customers = custom
            };

            return View(custMovie);
        }

        public ActionResult Movies()
        {
            var movies = new List<Movie>
            {
                new Movie {Name="Shrek" },
                new Movie {Name="Wall-E" },
                new Movie {Name="Inception" }
            };

            var custMovie = new RandomMovieCustomer
            {
                movie = movies
            };

            return View(custMovie);
        }

        public ActionResult Index()
        {
            var mov = _context.Movies.Include(m => m.Genre).ToList();
            return View(mov);
        }

        public ActionResult Details(int Id)
        {
            var mov = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);
            return View(mov);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                   
                    Genres = _context.Genres.ToList()
                };
                return View("MoviesForm", viewModel);
            }

            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.Genre = movie.Genre;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();
            return RedirectToAction("Index","Movies");
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var movieViewModel = new MovieFormViewModel
            {
                
                Genres = genres
            };
            return View("MoviesForm",movieViewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            var movieModelView = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()

            };
            return View("MoviesForm",movieModelView);
        }

        
    }
}