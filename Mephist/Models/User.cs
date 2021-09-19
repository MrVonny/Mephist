using Mephist.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Mephist
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Group { get; set; }

        public virtual List<Media> Photos { get; set; } = new List<Media>();
        public virtual List<Review> Reviews { get; set; } = new List<Review>();


    }
}