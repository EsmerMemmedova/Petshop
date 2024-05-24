using Business.Exceptions;
using Business.Services.Abstarcts;
using Core.Models;
using Core.RepostoryAbstarcts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepostory _sliderRepostory;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderService(ISliderRepostory sliderRepostory, IWebHostEnvironment webHostEnvironment)
        {
            _sliderRepostory = sliderRepostory;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddSlider(Slider slider)
        {
            if (slider == null) throw new ArgumentNullException("Slider tapilmadi");
            if (slider.ImgFile == null) throw new FileNotFoundException("File tapilmadi");
            if (!slider.ImgFile.ContentType.Contains("image/")) throw new ContentTypeException("Type duzgun daxiil ediomeyib");
            if (slider.ImgFile.Length > 2097152) throw new FileSizeException("Filenin olchusu duzgun deyil");
            string path = _webHostEnvironment.WebRootPath + @"\Uploads\Sliders\" + slider.ImgFile.FileName;
            using(FileStream stream=new FileStream(path, FileMode.Create))
            {
                slider.ImgFile.CopyTo(stream);
            }
            slider.ImgUrl = slider.ImgFile.FileName;
            _sliderRepostory.Add(slider);
            _sliderRepostory.Commit();
        }

        public Slider GetSlider(Func<Slider, bool>? func = null)
        {
                return  _sliderRepostory.Get(func);

        }

        public List<Slider> GetSliderList(Func<Slider, bool>? func = null)
        {
            return _sliderRepostory.GetAll(func);
        }

      
        public void RemoveSlider(int id)
        {
            Slider oldslider=_sliderRepostory.Get(x=>x.Id== id);    
            if(oldslider == null) throw new ArgumentNullException("Slider tapilmadi");
            string oldpath = _webHostEnvironment.WebRootPath + @"\Uploads\Sliders\" + oldslider.ImgUrl;
            FileInfo fileInfo = new FileInfo(oldpath);
            if(fileInfo.Exists)
            {
                fileInfo.Delete();
            }
           _sliderRepostory.Remove(oldslider);
            _sliderRepostory.Commit();
        }
        public void UpdateSlider(int id, Slider slider)
        {
            Slider oldslider = _sliderRepostory.Get(x=> x.Id== id);
            if (oldslider == null) throw new ArgumentNullException("Slider tapilmadi");
            if (slider.ImgFile != null)
            {
                if (!slider.ImgFile.ContentType.Contains("image/") ) throw new ContentTypeException("Filenin type duzgun deyil");
                if (slider.ImgFile.Length > 2097152) throw new FileSizeException("Filenin olchusu duzgun deyil");
                string path = _webHostEnvironment.WebRootPath + @"\Uploads\Sliders\" + slider.ImgFile.FileName;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    slider.ImgFile.CopyTo(stream);
                }
                string oldpath = _webHostEnvironment.WebRootPath + @"\Uploads\Sliders\" + slider.ImgUrl;
                FileInfo fileInfo = new FileInfo(oldpath);
                if (fileInfo.Exists)
                    fileInfo.Delete();
                oldslider.ImgUrl = slider.ImgFile.FileName;
            }
            oldslider.FullName=slider.FullName;
            oldslider.Designation = slider.Designation;
            _sliderRepostory.Commit();
        }
        
    }
}
