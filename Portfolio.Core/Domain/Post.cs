using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Domain
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string body { get; set; } = "";
        public string Image { get; set; } = "";
        public DateTime created { get; set; } = DateTime.Now;
    }
}
