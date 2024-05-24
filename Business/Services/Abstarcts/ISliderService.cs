using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstarcts
{
    public interface ISliderService
    {
        void AddSlider(Slider slider);
        void UpdateSlider(int id, Slider slider);
        void RemoveSlider(int id);
        Slider GetSlider(Func<Slider, bool>? func = null);
        List<Slider> GetSliderList(Func<Slider,bool>? func=null);



    }
}
