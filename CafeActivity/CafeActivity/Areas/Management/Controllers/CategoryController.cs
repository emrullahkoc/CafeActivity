using CafeActivity.Models;
using CafeActivity.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeActivity.Areas.Management.Controllers
{
    [Area("Management")]

    public class CategoryController : Controller
    {
        CafeActivityContext db = new CafeActivityContext();
        private readonly IWebHostEnvironment _hostEnvironment;
        public CategoryController(IWebHostEnvironment hostEnviroment)
        {
            _hostEnvironment = hostEnviroment;
        }

        public IActionResult Index(string CategoryName)
        {
            ViewBag.Search = "";
            var model = db.Categories.Where(c => c.CategoryStatus == true).OrderByDescending(x => x.Id).ToList();
            if (!string.IsNullOrEmpty(CategoryName))
            {
                ViewBag.Search = CategoryName;
                model = model
                    .Where(c => c.CategoryName.ToLower().Contains(CategoryName.ToLower()) && c.CategoryStatus == true)
                    .ToList();
            }
            return View(model);
        }
        public async Task<JsonResult> DeleteByJs(int Id)
        {
            var Category = await db.Categories.FindAsync(Id);

            if (Category == null)
            {
                return Json("No Such Category Found");
            }

            Category.CategoryStatus = false;
            await db.SaveChangesAsync();

            return Json("Category Successfully Deleted");

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    model.CategoryImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                model.CategoryName = model.CategoryName.ToUpper();
                model.CategoryDescription = model.CategoryDescription;
                model.CategoryStatus = true;
                db.Categories.Add(model);
                db.SaveChanges();
                return Redirect("/Management/Category/Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category? model = db.Categories.Find(id);
            if (model == null)
            {
                return Redirect("/Management/Category/Index");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                Category? editmodel = db.Categories.Find(model.Id);
                if (editmodel == null)
                {
                    return Redirect("/Management/Category/Index");
                }
                if (img != null)
                {
                    await ImageUploader.DeleteImageAsync(_hostEnvironment, editmodel.CategoryImageUrl);
                    editmodel.CategoryImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                editmodel.CategoryName = model.CategoryName.ToUpper(); ;
                editmodel.CategoryDescription = model.CategoryDescription;
                editmodel.CategoryStatus = true;
                await db.SaveChangesAsync();
                return Redirect("/Management/Category/Index");
            }
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Category? model = db.Categories.Include(a => a.Activities.Where(a => a.ActivityStatus == true)).ThenInclude(b => b.Artist).FirstOrDefault(a => a.Id == id); ;
            if (model == null)
            {
                return Redirect("/Management/Category/Index");
            }
            return View(model);
        }
    }
}
