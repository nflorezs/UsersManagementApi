using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public enum EnumPagFilterParams
    {
        [Description("@PageSize")]
        PageSize,

        [Description("@PageNumber")]
        PageNumber,
    }
}
