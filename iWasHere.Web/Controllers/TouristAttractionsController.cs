using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using iWasHere.Domain.DTOs;
using Microsoft.AspNetCore.Hosting;
using iWasHere.Domain.Service;

namespace iWasHere.Controllers
{
    public class TouristAttractionsController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
     
        private readonly DictionaryService _dictionaryService;
        private readonly DatabaseContext _context;

      
        public TouristAttractionsController(DatabaseContext context,IHostingEnvironment hostingEnvironmentt,DictionaryService service)
        {
            hostingEnvironment = hostingEnvironmentt;
            _context = context;
            _dictionaryService = service;
        }

        // GET: TouristAttractions
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.TouristAttraction.Include(t => t.Category).Include(t => t.City).Include(t => t.Landmark);
            return View(await databaseContext.ToListAsync());
        }

        // GET: TouristAttractions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var touristAttraction = await _context.TouristAttraction
                .Include(t => t.Category)
                .Include(t => t.City)
                .Include(t => t.Landmark)
                .FirstOrDefaultAsync(m => m.TouristAttractionId == id);
            if (touristAttraction == null)
            {
                return NotFound();
            }

            return View(touristAttraction);
        }

        // GET: TouristAttractions/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.DictionaryCategory, "DictionaryCategoryId", "DictionaryCategoryName");
            ViewData["CityId"] = new SelectList(_context.DictionaryCity, "DictionaryCityId", "DictionaryCityName");
            ViewData["LandmarkId"] = new SelectList(_context.DictionaryLandmarkType, "DictionaryItemId", "DictionaryItemCode");
            return View();
        }

        // POST: TouristAttractions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TouristAttractionId,Name,Description,CategoryId,CityId,LandmarkId,Longtitudine,Latitudine")] TouristAttraction touristAttraction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(touristAttraction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.DictionaryCategory, "DictionaryCategoryId", "DictionaryCategoryName", touristAttraction.CategoryId);
            ViewData["CityId"] = new SelectList(_context.DictionaryCity, "DictionaryCityId", "DictionaryCityName", touristAttraction.CityId);
            ViewData["LandmarkId"] = new SelectList(_context.DictionaryLandmarkType, "DictionaryItemId", "DictionaryItemCode", touristAttraction.LandmarkId);
            return View(touristAttraction);
        }

        // GET: TouristAttractions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var touristAttraction = await _context.TouristAttraction.FindAsync(id);
            if (touristAttraction == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.DictionaryCategory, "DictionaryCategoryId", "DictionaryCategoryName", touristAttraction.CategoryId);
            ViewData["CityId"] = new SelectList(_context.DictionaryCity, "DictionaryCityId", "DictionaryCityName", touristAttraction.CityId);
            ViewData["LandmarkId"] = new SelectList(_context.DictionaryLandmarkType, "DictionaryItemId", "DictionaryItemCode", touristAttraction.LandmarkId);
            return View(touristAttraction);
        }

        // POST: TouristAttractions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TouristAttractionId,Name,Description,CategoryId,CityId,LandmarkId,Longtitudine,Latitudine")] TouristAttraction touristAttraction)
        {
            if (id != touristAttraction.TouristAttractionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(touristAttraction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TouristAttractionExists(touristAttraction.TouristAttractionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.DictionaryCategory, "DictionaryCategoryId", "DictionaryCategoryName", touristAttraction.CategoryId);
            ViewData["CityId"] = new SelectList(_context.DictionaryCity, "DictionaryCityId", "DictionaryCityName", touristAttraction.CityId);
            ViewData["LandmarkId"] = new SelectList(_context.DictionaryLandmarkType, "DictionaryItemId", "DictionaryItemCode", touristAttraction.LandmarkId);
            return View(touristAttraction);
        }

        // GET: TouristAttractions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var touristAttraction = await _context.TouristAttraction
                .Include(t => t.Category)
                .Include(t => t.City)
                .Include(t => t.Landmark)
                .FirstOrDefaultAsync(m => m.TouristAttractionId == id);
            if (touristAttraction == null)
            {
                return NotFound();
            }

            return View(touristAttraction);
        }

        // POST: TouristAttractions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var touristAttraction = await _context.TouristAttraction.FindAsync(id);
            _context.TouristAttraction.Remove(touristAttraction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TouristAttractionExists(int id)
        {
            return _context.TouristAttraction.Any(e => e.TouristAttractionId == id);
        }
        [HttpGet]
        public ActionResult Image()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Image(ImageModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                // If the Photo property on the incoming model object is not null, then the user
                // has selected an image to upload.
                if (model.Photo != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Image img = new Image
                {
                 TouristAttractionId=2,
                    Path = uniqueFileName
                };

                _dictionaryService.AddImage(img);
                
            }

            return View();
        }
        [HttpGet]
        public ActionResult AddImages()
        {
            return View();
        }


      
        [HttpPost]
        public IActionResult AddImages(IFormFile[] photos)
        {
            if (photos == null || photos.Length == 0)
            {
                return Content("File(s) not selected");
            }
            else
            {
          
                foreach (IFormFile photo in photos)
                {
                    string uniqueFileName = null;
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                 
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                   photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    

                    Image img = new Image
                    {
                        TouristAttractionId = 2,
                        Path = uniqueFileName
                    };
                    _dictionaryService.AddImage(img);
                }
            }
           
            return View();
        }
    }
}
