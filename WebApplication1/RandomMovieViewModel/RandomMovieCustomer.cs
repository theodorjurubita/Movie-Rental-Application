using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.RandomMovieViewModel
{
    public class RandomMovieCustomer
    {
        public List<Movie> movie { get; set; }
        public List<Customer> customers { get; set; }
    }
}