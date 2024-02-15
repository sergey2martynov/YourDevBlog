using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Blog
{
    public class GetPostDto
    {
        public Guid Id { get; set; }
        public string Preview { get; set; }
        public string Title { get; set; }
    }
}
