using System;
using System.Collections.Generic;

namespace MovieAPI.Models
{
    public partial class MovieList
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Year { get; set; }
        public byte[]? Picture { get; set; }
    }
}
