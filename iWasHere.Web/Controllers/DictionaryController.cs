using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public IActionResult Index()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dictionaryService.GetDictionaryLandmarkTypeModels();

            return View(dictionaryLandmarkTypeModels);
        }



        public IActionResult Schedule()
        {
            return View();
        }


        public IActionResult ScheduleData([DataSourceRequest]DataSourceRequest request)
        {
            List<ScheduleTouristAttractionModel> jsonVariable = _dictionaryService.GetDictionaryScheduleModels(request.Page, request.PageSize);
          
            Kendo.Mvc.UI.DataSourceResult v3 = new DataSourceResult();
            v3.Data = jsonVariable;
            v3.Total = _dictionaryService.GetItemsOfSchedule();
            return Json(v3);

        }



        //public IActionResult ScheduleFiltered([DataSourceRequest]DataSourceRequest request)
        //{
        //    //var jsonVariable = _dictionaryService.GetDictionaryScheduleFiltred().ToDataSourceResult(request);
        //   // return Json(jsonVariable);
        //}
        public IActionResult County()
        {
            return View();

        }

    }
}