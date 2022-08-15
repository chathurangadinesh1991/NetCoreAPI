using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreAPI.Domain.Models
{
    public class SaveDataRequest
    {
        public string UserId { get; set; }

        public string Post { get; set; }
    }
}
