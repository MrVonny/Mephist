﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mephist.Models.Intefaces
{
    public interface IUploadInfo
    {
        public User User { get; }
        public DateTime CreatedDate { get; }
        

    }
}