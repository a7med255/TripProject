using Microsoft.AspNetCore.Mvc;
using TripProject.Bl;
using TripProject.Models;
using TripProject.Utilities;

namespace TripProject.Areas.admin.Controllers
{
    [Area("admin")]
    public class RequestsController : Controller
    {
        public RequestsController(IRequests requests,ICategories categories,ICity city,ITrips trips)
        {
            ClsRequest =requests;
            ClsCategories=categories;
            ClsCity=city;
            ClsTrips=trips;
        }
        IRequests ClsRequest;
        ICategories ClsCategories;
        ICity ClsCity;
        ITrips ClsTrips;

        public IActionResult List()//List Requests for book tickit
        {
            VwRequest request = new VwRequest();
            request.lstCity = ClsCity.GetAll(null);
            request.lstCategories = ClsCategories.GetAll();
            request.VwTrips = ClsTrips.GetAllItemsData(null,null).ToList();
            request.lstRequest = ClsRequest.GetAll();
            return View("List", request);
        }
        public IActionResult Search(int? categoryId, int? cityId)//Search request => categoryId, cityId
        {
            VwRequest request = new VwRequest();
            request.lstCity = ClsCity.GetAll(null);
            request.lstCategories = ClsCategories.GetAll();
            request.VwTrips = ClsTrips.GetAllItemsData(categoryId, cityId).ToList();
            request.lstRequest = ClsRequest.GetAllRequsetData(categoryId, cityId);
            return View("List", request);
        }
        public IActionResult Edit(int? id)
        {
            var request = new TbRequest();
            if (id != null)
            {
                request = ClsRequest.GetById(Convert.ToInt32(id));
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbRequest request, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", request);
            request.Image = await Helper.UploadImage(Files, "Users");
            ClsRequest.Save(request);

            return RedirectToAction("List");
        }
        public IActionResult Details(int id)
        {
            var request = ClsRequest.GetById(id);
            return View(request);
        }
        public IActionResult Delete(int id)
        {
            ClsRequest.Delete(id);
            return RedirectToAction("List");

        }
    }
}
