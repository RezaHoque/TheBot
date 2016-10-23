using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBot.Models
{
    public class Luis
    {
        public string query { get; set; }
        public Intent[] intents { get; set; }
        public Entity[] entities { get; set; }
        public Dialog dialog { get; set; }
        public Action[] actions { get; set; }
    }
}