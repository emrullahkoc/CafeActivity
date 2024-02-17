using CafeActivity.Models;
using CafeActivity.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CafeActivity.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize("Admin")]
    public class ActivityController : Controller
    {
        CafeActivityContext db = new CafeActivityContext();

        private readonly IWebHostEnvironment _hostEnvironment;
        public ActivityController(IWebHostEnvironment hostEnviroment)
        {
            _hostEnvironment = hostEnviroment;
        }

        public IActionResult Index(string ActivityName)
        {
            ViewBag.Search = "";
            var model = db.Activities.Where(c => c.ActivityStatus == true).OrderByDescending(x => x.CreateDate).Include(x => x.Category).Include(x=> x.Artist).ToList();
            if (!string.IsNullOrEmpty(ActivityName))
            {
                ViewBag.Search = ActivityName;
                model = model
                    .Where(c => c.ActivityName.ToLower().Contains(ActivityName.ToLower()) && c.ActivityStatus == true)
                    .ToList();
            }
            return View(model);
        }
        public async Task<JsonResult> DeleteByJs(int Id)
        {
            var Activity = await db.Activities.FindAsync(Id);

            if (Activity == null)
            {
                return Json("No Such Activity Found");
            }

            Activity.ActivityStatus = false;
            await db.SaveChangesAsync();

            return Json("Activity Successfully Deleted");
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(x => x.CategoryName), "Id", "CategoryName", null);
            ViewBag.ArtistId = new SelectList(db.Artists.OrderBy(x => x.ArtistName), "Id", "ArtistName", null);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Activity model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    model.ActivityImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                model.ActivityName = model.ActivityName.ToUpper();
                model.ActivityStatus = true;
                model.ActivityDescription = model.ActivityDescription;
                model.ActivityDate = model.ActivityDate;
                model.ActivityPrice = model.ActivityPrice;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                db.Activities.Add(model);
                db.SaveChanges();
                return Redirect("/Management/Activity/Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", model.CategoryId);
            ViewBag.ArtistId = new SelectList(db.Artists, "Id", "ArtistName", model.ArtistId);
            return View(model);
        }
    }
}
