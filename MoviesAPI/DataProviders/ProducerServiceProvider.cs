using IMDBClone_API.DataContract;
using MoviesAPI.Context;
using MoviesData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBClone_API.DataProviders
{
    public class ProducerServiceProvider : IProducerService
    {
        private readonly MovieContext _context;

        public ProducerServiceProvider(MovieContext context)
        {            
            _context = context ?? throw new ArgumentNullException(nameof(context));            
        }
        public bool ProducerExist(string name, string company, string gender, DateTime dob)
        {
            Producer producer = _context.Producers.Where(m => m.Name == name
                                                         && m.Gender == gender
                                                         && m.Company == company
                                                         && m.DateOfBirth == dob).FirstOrDefault();
            if(producer == null)
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
