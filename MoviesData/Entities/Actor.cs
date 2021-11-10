using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesData.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }        
        public string Gender { get; set; }
        public IList<ActorMovie> ActorMovies { get; set; }
    }
}
