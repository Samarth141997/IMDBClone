using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBClone_API.DataContract
{
    public interface IActorService
    {
        bool ActorExist(string name, string gender, DateTime dob);
    }
}
