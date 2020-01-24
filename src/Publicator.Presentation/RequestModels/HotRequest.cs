using Publicator.ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publicator.Presentation.RequestModels
{
    public class HotRequest : PageRequest
    {
        public HotPeriod? Period { get; set; }
    }
}
