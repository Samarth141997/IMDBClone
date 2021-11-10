using System;
using System.Collections.Generic;
using System.Text;

namespace IMDBClone_Data.DataModels
{
    public class MovieWithInfoViewModel
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string Genre { get; set; }
        public string Producer { get; set; }
        public List<string> Actors { get; set; }
    }
}
