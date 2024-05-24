using Business.Services.Abstarcts;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace PetShop.Controllers
{
    public class HomeController : Controller
    {
        ISliderService _sliderService;

        public HomeController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            return View(_sliderService.GetSliderList());
        }

    }
}
