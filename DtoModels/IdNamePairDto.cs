using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyAPI.DtoModels
{
    public class IdNamePairDto
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
    }
}