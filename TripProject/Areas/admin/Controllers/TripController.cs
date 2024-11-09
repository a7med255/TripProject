using TripProject.Bl;
using TripProject.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripProject.Models;

namespace TripProject.Areas.admin.Controllers
{
    [Area("admin")]
    public class TripController : Controller
    {
        TripsContext context;
        public TripController(ITrips trips, ICategories categories, ICity city, ICountry country, TripsContext ctx)
        {
            ClsTrips = trips;
            ClsCategories = categories;
            ClsCity = city;
            ClsCountry = country;
            context = ctx;
        }

        ITrips ClsTrips;
        ICategories ClsCategories;
        ICity ClsCity;
        ICountry ClsCountry;

        public IActionResult List()
        {
            ViewBag.lstCity = ClsCity.GetAll(null);
            ViewBag.lstCategories = ClsCategories.GetAll();
            return View(ClsTrips.GetAllItemsData(null, null));
        }
        public IActionResult Search(int categoryId, int cityId)
        {
            ViewBag.lstCity = ClsCity.GetAll(null);
            ViewBag.lstCategories = ClsCategories.GetAll();
            return View("List", ClsTrips.GetAllItemsData(categoryId, cityId));
        }
        public IActionResult Edit(int? id)
        {
            var trip = new Models.TbTrip();
            ViewBag.lstCategories = ClsCategories.GetAll();
            ViewBag.lstCity = ClsCity.GetAll(null);

            if (id != null)
            {
                trip = ClsTrips.GetById(Convert.ToInt32(id));
            }
            return View(trip);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbTrip trip, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", trip);
            trip.Image = await Helper.UploadImage(Files, "Trips");
            ClsTrips.Save(trip);

            return RedirectToAction("List");
        }



        public IActionResult Delete(int id)
        {
            ClsTrips.Delete(id);
            return RedirectToAction("List");

        }
    }
}
