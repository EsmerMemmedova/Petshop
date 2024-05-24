using Business.Exceptions;
using Business.Services.Abstarcts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace PetShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            List<Slider>sliders=_sliderService.GetSliderList();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _sliderService.AddSlider(slider);
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error");
                return View();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var oldslider =_sliderService.GetSlider(x=>x.Id==id);
            if (oldslider == null) { 

                ModelState.AddModelError("", "Doctor is Null!");
            return RedirectToAction("Index");

            }
            return View(oldslider);
         
        
        }
        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _sliderService.UpdateSlider(slider.Id, slider);
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("","Error");
                return View();
            }
          return RedirectToAction(nameof(Index));
            
        }
        public IActionResult Delete(int id)
        {
            _sliderService.RemoveSlider(id);
            return RedirectToAction("Index");
        }

    }
}    
