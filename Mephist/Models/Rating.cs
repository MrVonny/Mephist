using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace Mephist.Models
{

    public class Rating
    {

        [Key]
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [DefaultValue(0)]
        public int CharacterScore { get; set; }
        [DefaultValue(0)]
        public int TeachingScore { get; set; }
        [DefaultValue(0)]
        public int ExamsScore { get; set; }

        [DefaultValue(0)]
        public int CharacterVotes { get; set; }
        [DefaultValue(0)]
        public int TeachingVotes { get; set; }
        [DefaultValue(0)]
        public int ExamsVotes { get; set; }
    }
      
}
