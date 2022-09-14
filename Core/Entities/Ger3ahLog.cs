using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Ger3ahLog
    {
        public int Id { get; set; }
        public string  PickerName { get; set; }
        public string PickedName { get; set; }
        public DateTime CraetedDate { get; set; }
    }
}