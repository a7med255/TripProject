using Microsoft.AspNetCore.Mvc;
using TripProject.Bl;
using TripProject.Models;
using TripProject.Utilities;
namespace TripProject.Areas.admin.Controllers
{
    [Area("admin")]
    public class CityController : Controller
    {

        public CityController(ICity city,ICountry country)
        {
            ClsCity = city;
            ClsCountry = country;
        }
        ICity ClsCity;
        ICountry ClsCountry;

        public IActionResult List()//List City
        {
            ViewBag.lstCountry = ClsCountry.GetAll();
            return View(ClsCity.GetAll(null));
        }
        public IActionResult Search(int countryId)//Search City=> countryId 
        {
            ViewBag.lstCountry = ClsCountry.GetAll();
            return View("List", ClsCity.GetAll(countryId));
        }
        public IActionResult Edit(int? cityid)//Edit or Add New City
        {
            var city = new TbCity();
            ViewBag.lstCountry = ClsCountry.GetAll();
            if (cityid != null)
            {
                city = ClsCity.GetById(Convert.ToInt32(cityid));
            }
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbCity city)//Save City
        {
            if (!ModelState.IsValid)
                return View("Edit", city);

            ClsCity.Save(city);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int cityid)//Delete City
        {
            ClsCity.Delete(cityid);
            return RedirectToAction("List");

        }

    }
}
