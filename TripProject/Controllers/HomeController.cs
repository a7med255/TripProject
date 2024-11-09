using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using TripProject.Bl;
using TripProject.Models;
using TripProject.Utilities;

namespace TripProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        TripsContext context;
        public int? m;
        
        public HomeController(ILogger<HomeController> logger ,ITrips trips,IRequests requests,TripsContext ctx )
        {
            _logger = logger;
            ClsTrips= trips;
            ClsRequest= requests;
            context = ctx;
        }
        ITrips ClsTrips;
        IRequests ClsRequest;

        public class DetailViewModel
        {
            public VwTrip VwTrips { get; set; }=new VwTrip();
        }
        public IActionResult Index()//Home page =>new trips,all Trips randomly,best saller trips 
        {
            VwHome vwHome = new VwHome();
            vwHome.lstNewTrips=ClsTrips.GetAllItemsData(null,null).OrderByDescending(a=>a.CreatedDate).Take(8).ToList();
            vwHome.lstRandomlyTrips = ClsTrips.GetAllItemsData(null, null).OrderBy(a => Guid.NewGuid()).ToList();
            vwHome.lstBestSallerTrips = ClsTrips.GetAllItemsData(null, null).Where(a=>a.BestSellerRequest>0).OrderByDescending(a=>a.BestSellerRequest).Take(8).ToList();

            return View(vwHome);
        }
        public IActionResult Details(int? id)//detail in page card and to request to book tickit
        {
            VwDetail vwDetail = new VwDetail
            {
                TbRequest = new TbRequest(),
                vwTrips = ClsTrips.GetAllItemsData(null, null).FirstOrDefault(a => a.Id == id)
            };

            return View(vwDetail); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, VwDetail vwDetail, List<IFormFile> Files)//to save request in db
        {
            vwDetail.TbRequest.RequestId = id;

            if (ModelState.IsValid)
            {
                vwDetail.TbRequest.Image = await Helper.UploadImage(Files, "Users");
                ClsRequest.Save(vwDetail.TbRequest); // Save request data
                var trip= context.TbTrips.FirstOrDefault(a => a.Id == id);
                trip.BestSellerRequest += 1;
                context.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = vwDetail.TbRequest.RequestId }); // Redirect to details page
            }
            vwDetail.vwTrips = ClsTrips.GetAllItemsData(null, null).FirstOrDefault(a => a.Id == id);
            
            return View(vwDetail);
        }
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyEmail(string Email)
        {
            if (false)
            {
                return Json($"Email {Email} is already in use.");
            }

            return Json(true);
        }
        [AcceptVerbs("GET", "POST")]
        public IActionResult IsPhoneUnique(string phoneNumber)
        {
            if (false)
            {
                return Json($"Phone number '{phoneNumber}' is already in use.");
            }
            return Json(true);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
