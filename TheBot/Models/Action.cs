using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBot.Models
{
    public class Action
    {
        public string triggered { get; set; }
        public string name { get; set; }
        public Parameter parameters { get; set; }

    }
}