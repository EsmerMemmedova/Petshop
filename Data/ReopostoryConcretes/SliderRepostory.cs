using Core.Models;
using Core.RepostoryAbstarcts;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ReopostoryConcretes
{
    public class SliderRepostory : GenericRepostory<Slider>, ISliderRepostory
    {
        public SliderRepostory(AppDbContext context) : base(context)
        {
        }
    }
}
