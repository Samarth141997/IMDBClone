using IMDBClone_Data.DataModels;
using MoviesData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBClone_API.DataContract
{
    public interface IMovieService
    {
        List<MovieWithInfoViewModel> GetMoviesWithInfo();      
        void CreateMovie(CreateMovieModel model);
        Movie GetMovie(int id);
        bool MovieExists(string name, string genre, string details);
        bool Save();
    }
}
