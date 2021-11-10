using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IMDBClone_Data.DataModels
{
    public class CreateMovieModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }
        public string Details { get; set; }
        [Required]
        public List<ActorViewModel> Actors { get; set; }
        [Required]
        public ProducerViewModel Producer { get; set; }
    }
}
