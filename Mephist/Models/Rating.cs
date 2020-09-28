using System;
using System.Collections.Generic;
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

        public int CharacterScore { get; set; }
        public int TeachingScore { get; set; }
        public int ExamsScore { get; set; }

        public int CharacterVotes { get; set; }
        public int TeachingVotes { get; set; }
        public int ExamsVotes { get; set; }
    }
      
}
