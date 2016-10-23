using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBot.Models
{
    public class Dialog
    {
        public string prompt { get; set; }
        public string parameterName { get; set; }
        public string parameterType { get; set; }
        public string contextId { get; set; }
        public string status { get; set; }

    }
}