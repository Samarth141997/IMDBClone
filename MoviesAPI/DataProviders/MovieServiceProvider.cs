using IMDBClone_API.DataContract;
using IMDBClone_Data.DataModels;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Context;
using MoviesData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBClone_API.DataProviders
{
    public class MovieServiceProvider : IMovieService
    {
        private readonly IProducerService _producerService;
        private readonly IActorService _actorService;
        private readonly MovieContext _moviesContext;

        public MovieServiceProvider(MovieContext moviesContext, IActorService actorService, IProducerService producerService)
        {                        
            _producerService = producerService ?? throw new ArgumentNullException(nameof(producerService));            
            _actorService = actorService ?? throw new ArgumentNullException(nameof(actorService));            
            _moviesContext = moviesContext ?? throw new ArgumentNullException(nameof(moviesContext));            
        }
        public void CreateMovie(CreateMovieModel model)
        {
            Movie movie = new Movie();
            if(!_producerService.ProducerExist(model.Producer.Name,model.Producer.Company,model.Producer.Gender,model.Producer.DateOfBirth))
            {
                movie.Name = model.Name;
                movie.Genre = model.Genre;
                movie.Details = model.Details;
                movie.Producer = new Producer
                {
                    Name = model.Producer.Name,
                    Gender = model.Producer.Gender,
                    Company = model.Producer.Company,
                    DateOfBirth = model.Producer.DateOfBirth
                };                
            }     
            else
            {
                int producerId = _moviesContext.Producers.Where(m => m.Name == model.Producer.Name
                                                                && m.Gender == model.Producer.Gender
                                                                && m.Company == model.Producer.Company
                                                                && m.DateOfBirth == model.Producer.DateOfBirth)
                                                                .Select(m => m.Id).FirstOrDefault();
                movie.Name = model.Name;
                movie.Genre = model.Genre;
                movie.Details = model.Details;
                movie.ProducerId = producerId;
            }
            foreach(var actor in model.Actors)
            {
                if(!_actorService.ActorExist(actor.Name, actor.Gender, actor.DateOfBirth))
                {
                    _moviesContext.Actors.Add(new Actor
                    {
                        Name = actor.Name,
                        Gender = actor.Gender,
                        DateOfBirth = actor.DateOfBirth
                    });
                } 
                else
                {
                    int actorId = _moviesContext.Actors.Where(m => m.Name == actor.Name
                                                              && m.Gender == actor.Gender
                                                              && m.DateOfBirth == actor.DateOfBirth)
                                                              .Select(m => m.Id).FirstOrDefault();
                    movie.ActorMovies.Add(new ActorMovie { ActorId = actorId});
                }
            }            
            _moviesContext.Movies.Add(movie);
            Save();
            int movieId = _moviesContext.Movies.Where(m => m.Name == model.Name
                                                      && m.Genre == model.Genre
                                                      && m.Details == model.Details)
                                                      .Select(m => m.MovieId).First();
            foreach(var actor in model.Actors)
            {
                int existingActorId = _moviesContext.Actors.Where(m => m.Name == actor.Name
                                                                && m.Gender == actor.Gender
                                                                && m.DateOfBirth == actor.DateOfBirth)
                                                                .Select(m => m.Id).First();
                _moviesContext.ActorMovies.Add(new ActorMovie { MovieId = movieId, ActorId = existingActorId });
            }
            Save();
            

        }        

        public List<MovieWithInfoViewModel> GetMoviesWithInfo()
        {
            List<MovieWithInfoViewModel> moviesInfo = new List<MovieWithInfoViewModel>();
            var movies = _moviesContext.Movies.Include(m => m.Producer).Include(m => m.ActorMovies).ThenInclude(m => m.Actor).ToList();                       
            foreach(var movie in movies)
            {
                List<string> actorNames = new List<string>();
                movie.ActorMovies.All(m =>
                {
                    actorNames.Add(m.Actor.Name);
                    return true;
                });
                moviesInfo.Add(new MovieWithInfoViewModel
                {
                    Name = movie.Name,
                    Actors = actorNames,
                    Details = movie.Details,
                    Genre = movie.Genre,
                    Producer = movie.Producer.Name
                });               
            }
            return moviesInfo;
        }

        public bool Save()
        {
            return (_moviesContext.SaveChanges() >= 0);
        }

        public bool MovieExists(string name, string genre, string details)
        {
            Movie movie = _moviesContext.Movies.Where(m => m.Name == name
                                                      && m.Genre == genre
                                                      && m.Details == details)
                                                      .FirstOrDefault();
            if(movie == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Movie GetMovie(int id)
        {
            Movie existingMovie = _moviesContext.Movies.Where(m => m.MovieId == id).Include(m => m.Producer).Include(m => m.ActorMovies).ThenInclude(m => m.Actor).FirstOrDefault();                            
            return existingMovie;
        }
    }
}
