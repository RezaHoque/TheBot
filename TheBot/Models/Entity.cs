using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBot.Models
{
    public class Entity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public string startIndex { get; set; }
        public string endIndex { get; set; }
        public float score { get; set; }
    }
}