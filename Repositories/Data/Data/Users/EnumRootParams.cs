using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public enum EnumRootParams
    {
        [Description("@actual_page")]
        ActualPage,

        [Description("@page")]
        Page,

        [Description("@per_page")]
        PerPage,

        [Description("@total")]
        Total,

        [Description("@total_pages")]
        TotalPages,

        [Description("@updated")]
        Updated,
    }
}
