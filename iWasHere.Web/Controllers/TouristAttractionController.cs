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
        public IActionResult AddReview(TouristAttractionMapsModel modelMaps, string submit)
        {
            //ReviewModel modelReview = null;
            //if (modelMaps.TouristAttractionId != null)
            //{
            //    string errorMessage;
            //    if (modelMaps==0)
            //  {
            //       modelReview = new ReviewModel()
            //     {
            //        User = modelMaps.Review.User,
            //        RatingValue = modelMaps.Review.RatingValue,
            //        Comment = modelMaps.Review.Comment,
            //        Title = modelMaps.Review.Title,
            //        TouristAttractionId = modelMaps.Review.TouristAttractionId
            //      };
            //}
            //string errorMessage = _dictionaryService.InsertReview(modelReview);
            //if (!String.IsNullOrEmpty(errorMessage))
            //{
            //    ModelState.AddModelError("e", errorMessage);
            //    return View(modelMaps);
            //}
            //else
            //{
            //    return View();
            //}

            if (submit == "save")
            {
                string errorMessage;
                ReviewModel modelReview = new ReviewModel()
                {
                    User = modelMaps.Review.User,
                    RatingValue = modelMaps.Review.RatingValue,
                    Comment = modelMaps.Review.Comment,
                    Title = modelMaps.Review.Title,
                    TouristAttractionId = modelMaps.TouristAttractionId
                };
                errorMessage = _dictionaryService.InsertReview(modelReview);

                if (String.IsNullOrWhiteSpace(errorMessage))
                {
                    ModelState.Clear();
                    TouristAttractionMapsModel newDetails = _dictionaryService.GetTouristAttractionMapsById(modelMaps.TouristAttractionId);
                    return View("Details", newDetails);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View("Details", new { Id = modelMaps.TouristAttractionId });
                }
            }
            else
            {
                return RedirectToAction("Index", "TouristAttractions");
            }

        }



        public IActionResult Review(int Id)
        {
            return View();
        }


        //public IActionResult AddReview(int Id)
        //{
        //    if (Convert.ToInt32(Id) == 0)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        TouristAttractionMapsModel reviewModel = new TouristAttractionMapsModel();
        //        {
        //            reviewModel.TouristAttractionId= Convert.ToInt32(Id);
        //        }

        //        return View(reviewModel);
        //    }
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddReview(Review model, string btn, int Id)
        //{
        //    if (model != null)
        //    {
        //        string errorMessage = _dictionaryService.InsertReview(model);
        //        if (!String.IsNullOrEmpty(errorMessage))
        //        {
        //            ModelState.AddModelError("e", errorMessage);
        //            return View();
        //        }
        //    }
        //    return View();
        //}


        //public IActionResult TouristicObjectiveDetail(TouristicObjectiveDTO model, string feedbackName, int RatingName)
        //{
        //    FeedbackDTO modelFeedback = null;
        //    var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);// Specify the type of your UserId;
        //    if (model != null)
        //    {
        //        modelFeedback = new FeedbackDTO()
        //        {
        //            CommentTitle = model.FeedbackDTO.CommentTitle,
        //            Comment = model.FeedbackDTO.Comment,
        //            Rating = model.FeedbackDTO.Rating,
        //            TouristicObjectiveId = model.TouristicObjectiveId,
        //            UserName = model.FeedbackDTO.UserName,
        //            UserId = model.FeedbackDTO.UserId
        //        };
        //        string errorMessage = _dictionaryObjective.InsertFeedback(modelFeedback, userId, feedbackName, RatingName);
        //        if (!String.IsNullOrEmpty(errorMessage))
        //        {
        //            ModelState.AddModelError("e", errorMessage);
        //            return View(model);
        //        }
        //    }



        [HttpGet]
        public JsonResult GetObjectiveDetails(TouristAttractionTypeModel touristData)
        {
            return Json(new { result = _touristAttractionService.GetObjectiveDetails(touristData.Id) });
        }

    }
}
