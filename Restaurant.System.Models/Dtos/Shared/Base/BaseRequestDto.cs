using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.System.Models.Dtos.Shared.Base
{
    public class BaseRequestDto
    {
        public string UserAgent { get; set; }
        public string AppVersion { get; set; }
    }
}