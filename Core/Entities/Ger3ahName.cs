using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Ger3ahName
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string name { get; set; }
        public bool IsTaken { get; set; }

         public virtual User User { get; set; }
    }
}