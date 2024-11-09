using Microsoft.AspNetCore.Mvc;
using TripProject.Bl;
using TripProject.Models;
using TripProject.Utilities;
namespace TripProject.Areas.admin.Controllers
{
    [Area("admin")]
    public class CategoriesController : Controller
    {

        public CategoriesController(ICategories categories)
        {
            ClsCategories = categories;
        }
        ICategories ClsCategories;

        public IActionResult List()//List Categories
        {

            return View(ClsCategories.GetAll());
        }
        public IActionResult Edit(int? CategoryId)//Edit Or Add New Categories
        {
            var category = new TbCategory();
            if (CategoryId != null)
            {
                category = ClsCategories.GetById(Convert.ToInt32(CategoryId));
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbCategory category) //Save Categories
        {
            if (!ModelState.IsValid)
                return View("Edit", category);

            ClsCategories.Save(category);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int CategoryId)//Delete Categories
        {
            ClsCategories.Delete(CategoryId);
            return RedirectToAction("List");

        }

    }
}
