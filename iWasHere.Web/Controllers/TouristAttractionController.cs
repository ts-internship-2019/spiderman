using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Controllers
{
    public class TouristAttractionController : Controller
    {
        private readonly TouristAttractionService _touristAttractionService;

        public TouristAttractionController(TouristAttractionService touristAttractionService)
        {
            _touristAttractionService = touristAttractionService;
        }

        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetObjectiveDetails(TouristAttractionTypeModel touristData)
        {
            return Json(new { result = _touristAttractionService.GetObjectiveDetails(touristData.Id)});
        }

    }
}