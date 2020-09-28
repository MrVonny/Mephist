using Mephist.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mephist
{
    public class EducationalMaterial
    {
        public EducationMaterialType Type { get; set; }
        public List<Media> Materials { get; set; }

    }
}