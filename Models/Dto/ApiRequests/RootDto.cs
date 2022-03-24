using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiRequests
{
    public class RootDto
    {
        public DateTime updated { get; set; }
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public int actual_page { get; set; }
        public List<DatumDto>? data { get; set; }
        public SupportDto? support { get; set; }
    }
}
