using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Domain
{
    public class PostViewModel
    {
        public string Title { get; set; } = "";
        public string body { get; set; } = "";

        public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public string Category { get; set; } = "";


        public string CurrentImage { get; set; } = "";
        public IFormFile Image { get; set; } = null;
        public int Id { get; set; }
    }
}
