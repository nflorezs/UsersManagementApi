﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Root
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public int actual_page { get; set; }
        public List<Datum>? data { get; set; }
        public Support? support { get; set; }
        public DateTime updated { get; set; }
    }
}
