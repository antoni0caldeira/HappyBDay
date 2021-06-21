using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBDay.Models
{
    public class SelectDayViewModel
    {

        public int Day { get; set; }

        public int Month { get; set; }

        public List<Consultants> Consultants { get; set; }

    }
}

