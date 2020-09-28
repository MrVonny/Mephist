using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Models
{
    public class Media
    {
        public string MediaName { get; set; }
        public byte[] MediaFile { get; set; }
        public string MediaMimeType { get; set; }

    }
}
