using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using iWasHere.Web.Models;
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
        public IActionResult Currency()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dictionaryService.GetDictionaryLandmarkTypeModels();

            return View(dictionaryLandmarkTypeModels);
        }

        public IActionResult County()
        {
            return View();
        }
     
        public IActionResult City()
        {
            return View();
        }

        public JsonResult CountyData()
        {
          var JsonVariable= _dictionaryService.GetDictionaryCountiesCB();

            return Json(JsonVariable);

        }
        public JsonResult CountryData()
        {
            var JsonVariable = _dictionaryService.GetDictionaryCountriesCB();

            return Json(JsonVariable);

        }
        public ActionResult CityData([DataSourceRequest]DataSourceRequest request)
        {
            var jsonVariable = _dictionaryService.GetDictionaryCityModels(request.Page, request.PageSize).ToDataSourceResult(request);
            jsonVariable.Total = 2097152;
            return Json(jsonVariable);
           
        }
      
        public ActionResult GetCountyData([DataSourceRequest]DataSourceRequest request, string abc, int edf)
        {
  
                var jsonVariable = _dictionaryService.GetDictionaryCountyTypeModelsFilter(request.Page, request.PageSize, abc,edf,out int totalrows);
                DataSourceResult result = new DataSourceResult()
                {
                    Data = jsonVariable,
                    Total = totalrows /*_dictionaryService.FilterTotalCounties(abc, edf)*/
                };
                return Json(result);
            
        }

        public IActionResult GetCountry()
        {
            return View();
        }

        public JsonResult GetJsonCountryData()
        {
            return Json(_dictionaryService.GetAllCountries());
        }

        public ActionResult Category()
        {
            return View();
        }

        public ActionResult CategoryBinding_Read([DataSourceRequest] DataSourceRequest request)
        {
            var jsonVariable = _dictionaryService.GetDictionaryCategoryTypeModel(request.Page, request.PageSize);
            DataSourceResult result = new DataSourceResult()
            {
                Data = jsonVariable,
                Total = _dictionaryService.Total()
            };
            return Json(result);
        }

        public IActionResult SearchCountyName()
        {
            return View();
            
        }

        //public IActionResult GetCountyByName([DataSourceRequest]DataSourceRequest request, string name)
        //{
        //    return Json(_dictionaryService.GetDictionaryCountyTypeModelsFilter(request.Page, request.PageSize, name));
        //}


        public IActionResult ClientFiltering()
        {
            return View();
        }

        public ActionResult FilterGetCountries(string text)
        {
            return Json(_dictionaryService.Filter_GetCountries(text));
        }

        public IActionResult AddCounty()
        {
            return View();

        }

        public ActionResult Process_DestroyCounty([DataSourceRequest] DataSourceRequest request, DictionaryCountyTypeModel dictionaryCountyType)
        {
            if (dictionaryCountyType != null)
            {
                string errorMessage=_dictionaryService.DeleteCounty(dictionaryCountyType.Id);
                if (string.IsNullOrWhiteSpace(errorMessage))
                {
                    return Json(ModelState.ToDataSourceResult());
                }
                else
                {
                    ModelState.AddModelError("a", errorMessage);
                    return Json(ModelState.ToDataSourceResult()); 
                }
            }

            return Json(ModelState.ToDataSourceResult());

        }


    }
}