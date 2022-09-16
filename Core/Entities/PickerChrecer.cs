using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PickerChrecer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsAlreadyPickedName { get; set; }

        public virtual User User { get; set; }
    }
}