using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mephist
{
    public interface IUploadInfo
    {
        public User User { get; set; }
        public DateTime CreatedDate { get; }
        

    }
}