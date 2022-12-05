using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Domain.Comments
{
    public class SubComment : Comment
    {
        public int MainCommentId { get; set; }
    }
}
