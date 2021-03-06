﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Opis { get; set; }
        public string Useradd { get; set; }
        public string fotourl { get; set; }
    }


    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}