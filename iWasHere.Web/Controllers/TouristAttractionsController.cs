using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using Kendo.Mvc.UI;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;

namespace iWasHere.Controllers
{
    public class TouristAttractionsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly DictionaryService _dictionaryService;
        public TouristAttractionsController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TouristAttractions()
        {
            return View();
        }

        // De comentat

        //GET: TouristAttractions
        //public async Task<IActionResult> Index()
        //{
        //    var databaseContext = _context.TouristAttraction.Include(t => t.Category).Include(t => t.City).Include(t => t.Landmark);
        //    return View(await databaseContext.ToListAsync());
        //}

        // GET: TouristAttractions/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var touristAttraction = await _context.TouristAttraction
        //        .Include(t => t.Category)
        //        .Include(t => t.City)
        //        .Include(t => t.Landmark)
        //        .FirstOrDefaultAsync(m => m.TouristAttractionId == id);
        //    if (touristAttraction == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(touristAttraction);
        //}

        //// GET: TouristAttractions/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: TouristAttractions/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("TouristAttractionId,Name,Description,CategoryId,CityId,LandmarkId,Longtitudine,Latitudine")] TouristAttraction touristAttraction)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(touristAttraction);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.DictionaryCategory, "DictionaryCategoryId", "DictionaryCategoryName", touristAttraction.CategoryId);
        //    ViewData["CityId"] = new SelectList(_context.DictionaryCity, "DictionaryCityId", "DictionaryCityName", touristAttraction.CityId);
        //    ViewData["LandmarkId"] = new SelectList(_context.DictionaryLandmarkType, "DictionaryItemId", "DictionaryItemCode", touristAttraction.LandmarkId);
        //    return View(touristAttraction);
        //}

        //// GET: TouristAttractions/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var touristAttraction = await _context.TouristAttraction.FindAsync(id);
        //    if (touristAttraction == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.DictionaryCategory, "DictionaryCategoryId", "DictionaryCategoryName", touristAttraction.CategoryId);
        //    ViewData["CityId"] = new SelectList(_context.DictionaryCity, "DictionaryCityId", "DictionaryCityName", touristAttraction.CityId);
        //    ViewData["LandmarkId"] = new SelectList(_context.DictionaryLandmarkType, "DictionaryItemId", "DictionaryItemCode", touristAttraction.LandmarkId);
        //    return View(touristAttraction);
        //}

        //// POST: TouristAttractions/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("TouristAttractionId,Name,Description,CategoryId,CityId,LandmarkId,Longtitudine,Latitudine")] TouristAttraction touristAttraction)
        //{
        //    if (id != touristAttraction.TouristAttractionId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(touristAttraction);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TouristAttractionExists(touristAttraction.TouristAttractionId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.DictionaryCategory, "DictionaryCategoryId", "DictionaryCategoryName", touristAttraction.CategoryId);
        //    ViewData["CityId"] = new SelectList(_context.DictionaryCity, "DictionaryCityId", "DictionaryCityName", touristAttraction.CityId);
        //    ViewData["LandmarkId"] = new SelectList(_context.DictionaryLandmarkType, "DictionaryItemId", "DictionaryItemCode", touristAttraction.LandmarkId);
        //    return View(touristAttraction);
        //}

        //// GET: TouristAttractions/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var touristAttraction = await _context.TouristAttraction
        //        .Include(t => t.Category)
        //        .Include(t => t.City)
        //        .Include(t => t.Landmark)
        //        .FirstOrDefaultAsync(m => m.TouristAttractionId == id);
        //    if (touristAttraction == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(touristAttraction);
        //}

        //// POST: TouristAttractions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var touristAttraction = await _context.TouristAttraction.FindAsync(id);
        //    _context.TouristAttraction.Remove(touristAttraction);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TouristAttractionExists(int id)
        //{
        //    return _context.TouristAttraction.Any(e => e.TouristAttractionId == id);
        //}

        // De comentat

        public IActionResult TouristAttractionsGrid([DataSourceRequest] DataSourceRequest request, string txtFilterName)
        {
            List<TouristAttractionsDTO> myList = _dictionaryService.GetTouristAttractionsModel(request.Page, request.PageSize, txtFilterName);
            DataSourceResult v2 = new DataSourceResult();
            v2.Data = myList;
            v2.Total = _dictionaryService.GetCountTouristAttraction(txtFilterName);
            return Json(v2);
        }




        public ActionResult UpdateTouristAttractions([DataSourceRequest] DataSourceRequest request, DictionaryCurrencyModel currency)
        {
            //TO DO
            //if (currency != null)
            //{
            //    string errorMessage = _dictionaryService.DeleteCurrency(currency.DictionaryItemId);
            //    if (!string.IsNullOrWhiteSpace(errorMessage))
            //    {
            //        ModelState.AddModelError("a", errorMessage);
            //    }
            //}
            return Json(ModelState.ToDataSourceResult());
        }
        public IActionResult CreateEdit(int Id)
        {
            if (Convert.ToInt32(Id) == 0)
            {
                return View();
            }
            else
            {
                TouristAttraction touristAttraction = _dictionaryService.GetTouristAttractions(Id);
                touristAttraction.TouristAttractionId = Id;
                touristAttraction.Category = _dictionaryService.GetDictionaryCategory(touristAttraction.CategoryId);
                touristAttraction.City = _dictionaryService.GetDictionaryCity(touristAttraction.CityId);
                touristAttraction.Landmark = _dictionaryService.GetLandmark(touristAttraction.LandmarkId);
                return View(touristAttraction);
                //return View();
            }
        }
        public ActionResult GetCategory()
        {
            return Json(_dictionaryService.GetTouristAttractionsCategory());
        }
        public ActionResult GetSchedule()
        {
            return Json(_dictionaryService.GetTouristAttractionsSchedule());
        }
        public ActionResult GetCity(string txtName)
        {
            return Json(_dictionaryService.GetTouristAttractionsCity(txtName));
        }
        public ActionResult Save([DataSourceRequest] DataSourceRequest request, TouristAttraction tA, string submitValue)
        {
            if (tA.TouristAttractionId <= 0)
            {
                if (submitValue == "save")
                {
                    TouristAttraction touristAttraction = new TouristAttraction();
                    touristAttraction.TouristAttractionId = tA.TouristAttractionId;
                    touristAttraction.Name = tA.Name;
                    touristAttraction.Description = tA.Description;
                    touristAttraction.Longtitudine = tA.Longtitudine;
                    touristAttraction.Latitudine = tA.Latitudine;
                    touristAttraction.CityId = tA.CityId;
                    touristAttraction.LandmarkId = tA.LandmarkId;
                    touristAttraction.CategoryId = tA.CategoryId;
                    touristAttraction.City = _dictionaryService.GetDictionaryCity(tA.CityId);
                    touristAttraction.Category = _dictionaryService.GetDictionaryCategory(tA.CategoryId);
                    touristAttraction.Landmark = _dictionaryService.GetLandmark(tA.LandmarkId);

                    _dictionaryService.AddTouristAttractions(tA);
                    return View("Index");
                }
                else if (submitValue == "cancel")
                {
                    return View("Index");
                }
                else
                {
                    TouristAttraction touristAttraction = new TouristAttraction();
                    touristAttraction.TouristAttractionId = tA.TouristAttractionId;
                    touristAttraction.Name = tA.Name;
                    touristAttraction.Description = tA.Description;
                    touristAttraction.Longtitudine = tA.Longtitudine;
                    touristAttraction.Latitudine = tA.Latitudine;
                    touristAttraction.CityId = tA.CityId;
                    touristAttraction.LandmarkId = tA.LandmarkId;
                    touristAttraction.CategoryId = tA.CategoryId;
                    touristAttraction.City = _dictionaryService.GetDictionaryCity(tA.CityId);
                    touristAttraction.Category = _dictionaryService.GetDictionaryCategory(tA.CategoryId);
                    touristAttraction.Landmark = _dictionaryService.GetLandmark(tA.LandmarkId);

                    _dictionaryService.AddTouristAttractions(touristAttraction);
                    ModelState.Clear();
                    return View("CreateEdit");
                }
            }
            else
            {
                if (submitValue == "save")
                {
                    TouristAttraction touristAttraction = new TouristAttraction();
                    touristAttraction.TouristAttractionId = tA.TouristAttractionId;
                    touristAttraction.Name = tA.Name;
                    touristAttraction.Description = tA.Description;
                    touristAttraction.Longtitudine = tA.Longtitudine;
                    touristAttraction.Latitudine = tA.Latitudine;
                    touristAttraction.CityId = tA.CityId;
                    touristAttraction.LandmarkId = tA.LandmarkId;
                    touristAttraction.CategoryId = tA.CategoryId;
                    touristAttraction.City = _dictionaryService.GetDictionaryCity(tA.CityId);
                    touristAttraction.Category = _dictionaryService.GetDictionaryCategory(tA.CategoryId);
                    touristAttraction.Landmark = _dictionaryService.GetLandmark(tA.LandmarkId);

                    _dictionaryService.EditTouristAttractions(touristAttraction);
                    return View("Index");
                }
                else
                {
                    return View("Index");
                }
            }

        }
        public ActionResult GetLandmark(string txtName)
        {
            return Json(_dictionaryService.GetTouristAttractionsLandmark_v2());
        }
        public ActionResult DestroyTouristAttractions([DataSourceRequest] DataSourceRequest request, TouristAttractionsDTO tA)
        {
            if (tA != null)
            {
                string errorMessage = _dictionaryService.DeleteTouristAttraction(tA.TouristAttractionId);
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    ModelState.AddModelError("a", errorMessage);
                }
            }


            return Json(ModelState.ToDataSourceResult());
        }
    }
}
