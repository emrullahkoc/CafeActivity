using CafeActivity.Models;
using CafeActivity.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeActivity.Areas.Management.Controllers
{
    [Area("Management")]
    public class ArtistController : Controller
    {
        CafeActivityContext db = new CafeActivityContext();
        private readonly IWebHostEnvironment _hostEnvironment;
        public ArtistController(IWebHostEnvironment hostEnviroment)
        {
            _hostEnvironment = hostEnviroment;
        }

        public IActionResult Index(string ArtistName)
        {
            ViewBag.Search = "";
            var model = db.Artists.Where(c => c.ArtistStatus == true).OrderByDescending(x => x.CreateDate).ToList();
            if (!string.IsNullOrEmpty(ArtistName))
            {
                ViewBag.Search = ArtistName;
                model = model
                    .Where(c => c.ArtistName.ToLower().Contains(ArtistName.ToLower()) && c.ArtistStatus == true)
                    .ToList();
            }
            return View(model);
        }
        public async Task<JsonResult> DeleteByJs(int Id)
        {
            var Artist = await db.Artists.FindAsync(Id);

            if (Artist == null)
            {
                return Json("No Such Artist Found");
            }

            Artist.ArtistStatus = false;
            await db.SaveChangesAsync();

            return Json("Artist Successfully Deleted");

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Artist model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    model.ArtistImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                model.ArtistName = model.ArtistName.ToUpper();
                model.ArtistStatus = true;
                model.ArtistDescription = model.ArtistDescription;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                db.Artists.Add(model);
                db.SaveChanges();
                return Redirect("/Management/Artist/Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Artist? model = db.Artists.Find(id);
            if (model == null)
            {
                return Redirect("/Management/Artist/Index");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Artist model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                Artist? editmodel = db.Artists.Find(model.Id);
                if (editmodel == null)
                {
                    return Redirect("/Management/Artist/Index");
                }
                if (img != null)
                {
                    await ImageUploader.DeleteImageAsync(_hostEnvironment, editmodel.ArtistImageUrl);
                    editmodel.ArtistImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                editmodel.ArtistName = model.ArtistName.ToUpper(); ;
                editmodel.ArtistDescription = model.ArtistDescription;
                editmodel.ArtistBirthDate = model.ArtistBirthDate;
                editmodel.ArtistStatus = true;
                await db.SaveChangesAsync();
                return Redirect("/Management/Artist/Index");
            }
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Artist? model = db.Artists.Include(a => a.Activities.Where(a => a.ActivityStatus == true)).ThenInclude(b => b.Category).FirstOrDefault(a => a.Id == id); ;
            if (model == null)
            {
                return Redirect("/Management/Author/Index");
            }
            return View(model);
        }
    }
}
