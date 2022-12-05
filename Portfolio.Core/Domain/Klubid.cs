using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Domain
{
    public class Klubid
    {
        public string title { get; set; }
        public string kohtliigas { get; set; }
        public string logo { get; set; }
        public string mänge { get; set; }
        public string võite { get; set; }
        public string viike { get; set; }
        public string kaotusi { get; set; }
        public string väravaid { get; set; }
        public string punkte { get; set; }


        public string date { get; set; }
        public string teamleft { get; set; }
        public string teamright { get; set; }
        public string score { get; set; }
    }
}
