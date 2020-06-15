using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalRandkowy.API.Helpers
{
    public class UserParams
    {
        public const int MaxPageSize = 48;
        int pageSize=24;
        
        public int PageNumber { get; set; } = 1;
        public int PageSize { get=>pageSize; set=>pageSize=Math.Min(MaxPageSize, value); }
    }
}
