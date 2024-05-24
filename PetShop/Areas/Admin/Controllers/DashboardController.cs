using Business.Services.Abstarcts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PetShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {    
        ISliderService _sliderService;

        public DashboardController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            return View(_sliderService.GetSliderList());
        }
    }
}
