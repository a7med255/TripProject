using Microsoft.AspNetCore.Mvc;
using TripProject.Bl;
using TripProject.Models;
using TripProject.Utilities;
namespace TripProject.Areas.admin.Controllers
{
    [Area("admin")]
    public class CountryController : Controller
    {

        public CountryController(ICountry country)
        {
            ClsCountry = country;
        }
        ICountry ClsCountry;

        public IActionResult List()//List Country
        {

            return View(ClsCountry.GetAll());
        }
        public IActionResult Edit(int? CountryId)//Edit or Add New Country
        {
            var country = new TbCountry();
            if (CountryId != null)
            {
                country = ClsCountry.GetById(Convert.ToInt32(CountryId));
            }
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbCountry country)//Save Country
        {
            if (!ModelState.IsValid)
                return View("Edit", country);

            ClsCountry.Save(country);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int CountryId)//Delete Country
        {
            ClsCountry.Delete(CountryId);
            return RedirectToAction("List");

        }

    }
}
