using IMDBClone_API.DataContract;
using IMDBClone_Data.DataModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Context;
using MoviesData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBClone_API.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService, MovieContext context)
        {                        
            _context = context ?? throw new ArgumentNullException(nameof(context));            
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));            
        }

        [HttpGet(Name = "GelAllProducts")]
        public IActionResult GetMovies()
        {
            var movies = _movieService.GetMoviesWithInfo();
            return Ok(movies);
        }

        [HttpPost]
        public IActionResult CreateMovie([FromBody] CreateMovieModel model)
        {
            if(_movieService.MovieExists(model.Name, model.Genre, model.Details))
            {
                return BadRequest("Movie already exists");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest("Please provide required Information");
            }
            _movieService.CreateMovie(model);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] CreateMovieModel model)
        {            
            Movie existingMovie = _movieService.GetMovie(id);
            if(existingMovie == null)
            {
                return NotFound();
            }

            existingMovie.Name = model.Name;
            existingMovie.Genre = model.Genre;
            existingMovie.Details = model.Details;
            existingMovie.Producer = new Producer
            {
                Name = model.Producer.Name,
                Company = model.Producer.Company,
                Gender = model.Producer.Gender,
                DateOfBirth = model.Producer.DateOfBirth
            };            
            if(existingMovie.ActorMovies.Count == model.Actors.Count)
            {
                int index = 0;
                foreach(var actor in model.Actors)
                {
                    existingMovie.ActorMovies[index].Actor.Name = actor.Name;
                    existingMovie.ActorMovies[index].Actor.Gender = actor.Gender;
                    existingMovie.ActorMovies[index].Actor.DateOfBirth = actor.DateOfBirth;
                    index++;
                }
            }
            else if(existingMovie.ActorMovies.Count < model.Actors.Count)
            {
                int index = 0;
                int existingCount = existingMovie.ActorMovies.Count;
                foreach (var actor in model.Actors)
                {
                    if(existingCount > 0)
                    {
                        existingMovie.ActorMovies[index].Actor.Name = actor.Name;
                        existingMovie.ActorMovies[index].Actor.Gender = actor.Gender;
                        existingMovie.ActorMovies[index].Actor.DateOfBirth = actor.DateOfBirth;
                        index++;
                        existingCount--;
                    } 
                    else
                    {
                        existingMovie.ActorMovies.Add(new ActorMovie
                        {
                            Actor = new Actor
                            {
                                Name = actor.Name,
                                Gender = actor.Gender,
                                DateOfBirth = actor.DateOfBirth
                            }
                        });
                    }
                }
            }
            _movieService.Save();
            return NoContent();
        }
    }
}
