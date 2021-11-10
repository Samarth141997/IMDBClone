using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesData.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Details { get; set; }
        public IList<ActorMovie> ActorMovies { get; set; }
        public Producer Producer { get; set; }
        public int ProducerId { get; set; }
    }
}
