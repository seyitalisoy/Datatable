using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class SpecifiedLinksDto
    {
        public List<Link> Links { get; set; }
        public int LinkCount { get; set; }
    }
}
