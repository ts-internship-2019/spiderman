using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Controllers
{
    public class TouristAttractionController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        private readonly TouristAttractionService _touristAttractionService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddEmail()
        {
            return View();
        }

        public TouristAttractionController(DictionaryService _dictionaryService, TouristAttractionService touristAttractionService)
        {
            this._dictionaryService = _dictionaryService;
            _touristAttractionService = touristAttractionService;
        }

        public IActionResult Details(int Id)
        {
            TouristAttractionMapsModel maps = _dictionaryService.GetTouristAttractionMapsById(Id);
            return View(maps);
        }


        public IActionResult Review(int Id)
        {
            return View();
        }

       
        public IActionResult AddReview(int Id)
        {
            if (Convert.ToInt32(Id) == 0)
            {
                return View();
            }
            else
            {
                ReviewModel reviewModel = new ReviewModel();
                {
                    reviewModel.TouristAttractionId= Convert.ToInt32(Id);
                }

                return View(reviewModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(TouristAttractionMapsModel model, string btn, int Id)
        {
            if (model != null)
            {
                string errorMessage = _dictionaryService.InsertReview(model);
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    ModelState.AddModelError("e", errorMessage);
                    return View();
                }
            }
            return View();
        }

        
        [HttpGet]
        public JsonResult GetObjectiveDetails(TouristAttractionTypeModel touristData)
        {
            return Json(new { result = _touristAttractionService.GetObjectiveDetails(touristData.Id) });
        }







    }
}
