using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBClone_API.DataContract
{
    public interface IProducerService
    {
        bool ProducerExist(string name, string company, string gender, DateTime dob);
    }
}
