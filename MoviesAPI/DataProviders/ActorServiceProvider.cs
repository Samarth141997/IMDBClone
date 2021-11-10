using IMDBClone_API.DataContract;
using MoviesAPI.Context;
using MoviesData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBClone_API.DataProviders
{
    public class ActorServiceProvider : IActorService
    {
        private readonly MovieContext _context;

        public ActorServiceProvider(MovieContext context)
        {            
            _context = context ?? throw new ArgumentNullException(nameof(context));            
        }
        public bool ActorExist(string name, string gender, DateTime dob)
        {
            Actor actor = _context.Actors.Where(m => m.Name == name && m.DateOfBirth == dob && m.Gender == gender).FirstOrDefault();
            if(actor == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
