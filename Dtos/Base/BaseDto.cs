using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAppServer.Dtos.Base
{
    public class BaseDto
    {

        public required bool Status { get; set; }
        public dynamic? Data { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}