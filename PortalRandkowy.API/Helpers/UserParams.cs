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
        public int UserId { get; set; }
        public string Gender { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 100;
        public string ZodiacSing { get; set; } = "Wszystkie";
        public string OrderBy { get; set; }
    }
}
