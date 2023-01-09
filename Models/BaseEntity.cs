using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyAPI.Models
{
    public class BaseEntity
    {
        public DateTime LastUpdatedOn {get; set;} = DateTime.Now;
        public int LastUpdatedBy {get; set;}
    }
}