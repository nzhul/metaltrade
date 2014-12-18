using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Slides
{
    public class Slide
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public SlideImage Image { get; set; }
    }
}
