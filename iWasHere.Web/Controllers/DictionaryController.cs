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
            var JsonVariable = _dictionaryService.GetDictionaryCountiesCB();

            return Json(JsonVariable);

        }
        public JsonResult CountryData()
        {
            var JsonVariable = _dictionaryService.GetDictionaryCountriesCB();

            return Json(JsonVariable);

        }
        public ActionResult CityData([DataSourceRequest]DataSourceRequest request,string city,int county)
        {
         
                var jsonVariable = _dictionaryService.GetDictionaryCityData(request.Page, request.PageSize,city,county);
            DataSourceResult result = new DataSourceResult()
            {
                Data = jsonVariable,
                Total = _dictionaryService.FilterTotalCities(city,county)
                };
                return Json(result);
            

        }

       
        public IActionResult GetCountyData([DataSourceRequest]DataSourceRequest request)
        {
            //_dictionaryService.GetDictionaryCountyTypeModels();
            var jsonVariable = _dictionaryService.GetDictionaryCountyTypeModels(request.Page, request.PageSize).ToDataSourceResult(request);
            jsonVariable.Total = 2097152;
            return Json(jsonVariable);
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
        public IActionResult AddCity(int id) {
            if (Convert.ToInt32(id) == 0)
            {

                return View();
            }
            else
            {
                DictionaryCityModel dictionaryCity = _dictionaryService.GetCity(Convert.ToInt32(id));
                return View(dictionaryCity);
            }
        }
        public IActionResult CityNew() { return View(); }
       
        public ActionResult NewCity(DictionaryCityModel city, string submitButton)
        {
            if (submitButton == "cancel")
            {
                return View("City");
            }
            if (city.CityId <= 0)
            {
                _dictionaryService.InsertCity(city);
            }
            else
            {
                _dictionaryService.UpdateCity(city);
            }
            if (submitButton == "savenew")
            {
                ModelState.Clear();
                return View("AddCity");
            }
            else
            {
                return View("City");
            }
        }
        public ActionResult SaveCity(string city, int county)
        {
           DatabaseContext gf = new DatabaseContext();
            gf.DictionaryCity.Add(new Domain.Models.DictionaryCity
            {
               DictionaryCityName=city,
               DictionaryCountyId=12
            });
            return Json(gf.SaveChanges());
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

        public IActionResult GetCountyByName([DataSourceRequest]DataSourceRequest request, string name)
        {
            return Json(_dictionaryService.GetDictionaryCountyTypeModelsFilter(request.Page, request.PageSize, name));
        }
       
        public IActionResult ClientFiltering()
        {
            return View();
        }

        public ActionResult FilterGetCounties(string text)
        {
            return Json(_dictionaryService.Filter_GetCounties(text));
        }
        public IActionResult CreateOrEdit(string id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
               DictionaryCityModel dictionaryCity = _dictionaryService.GetCity(Convert.ToInt32(id));
                return View(dictionaryCity);
            }
        }
        public ActionResult DeleteCityData([DataSourceRequest] DataSourceRequest request, DictionaryCityModel city )
        {
            if (city != null)
            {
              string error=  _dictionaryService.DeleteCity(city.CityId);
                if (!String.IsNullOrWhiteSpace(error))
                {
                    ModelState.AddModelError("b", error);
                }
            }

            return Json(ModelState.ToDataSourceResult());

        }
    }
}