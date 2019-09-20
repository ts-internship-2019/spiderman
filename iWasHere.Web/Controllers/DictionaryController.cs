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


        public IActionResult ScheduleData([DataSourceRequest]DataSourceRequest request, string txtFilterName)
        {
            List<ScheduleTouristAttractionModel> jsonVariable = _dictionaryService.GetDictionaryScheduleModels(request.Page, request.PageSize,txtFilterName);
          
            Kendo.Mvc.UI.DataSourceResult v3 = new DataSourceResult();
            v3.Data = jsonVariable;
            v3.Total = _dictionaryService.GetItemsOfSchedule(txtFilterName);
            return Json(v3);

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
            var jsonVariable = _dictionaryService.GetDictionaryCityModels(request.Page, request.PageSize);
            DataSourceResult result = new DataSourceResult()
            {
                Data = jsonVariable,
                Total = _dictionaryService.TotalCity()
            };
            return Json(result);

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

        public ActionResult AddCategory()
        {
            return View();
        }

        public ActionResult CategoryBinding_Read([DataSourceRequest] DataSourceRequest request, string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                var jsonVariable = _dictionaryService.GetDictionaryCategoryTypeModel(request.Page, request.PageSize);

                DataSourceResult result = new DataSourceResult()
                {
                    Data = jsonVariable,
                    Total = _dictionaryService.Total()
                };
                
                return Json(result);
            }
            else
            {
                var jsonVariable = _dictionaryService.GetDictionaryCategoryTypeFilter(request.Page, request.PageSize, categoryName);

                DataSourceResult result = new DataSourceResult()
                {
                    Data = jsonVariable,
                    Total = _dictionaryService.FilterTotalCategory(categoryName)
                };

                return Json(result);
            }
        }

        public IActionResult SearchCountyName()
        {
            return View();
            
        }


        public IActionResult ClientFiltering()
        {
            return View();
        }

        public ActionResult FilterGetCountries(string text)
        {
            return Json(_dictionaryService.Filter_GetCountries(text));
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

        public JsonResult ScheduleFilteredDataSource([DataSourceRequest]DataSourceRequest request, ScheduleTouristAttractionModel model)
        {
            if (ModelState.IsValid)
            {
                var filter = model.TouristAttractionName;
            }




            var jsonVar = _dictionaryService.GetDictionaryScheduleFiltred(request.Page, request.PageSize, model.TouristAttractionName);
            DataSourceResult result = new DataSourceResult()
            {
                Data = jsonVar,
                Total = _dictionaryService.GetItemsOfSchedule()
            };
            return Json(result);

     }

        public IActionResult GetCountryData([DataSourceRequest] DataSourceRequest request, string abc)
        {
            int rows = 0;
            var x = _dictionaryService.GetCountryModel(request.Page, request.PageSize, out rows, abc);
            DataSourceResult dataSource = new DataSourceResult();
            dataSource.Data = x;
            dataSource.Total = rows;
            return Json(dataSource);
            
        }

        public ActionResult Process_Destroy([DataSourceRequest] DataSourceRequest request, ScheduleTouristAttractionModel schedule)
        {
            if (schedule != null)
            {
                 string errorMessageForClient=  _dictionaryService.DeleteSchedule(schedule.ScheduleId);
                if (string.IsNullOrWhiteSpace(errorMessageForClient))
                {
                    return Json(ModelState.ToDataSourceResult());
                }
                else
                {
                    ModelState.AddModelError("a", errorMessageForClient);
                    return Json(ModelState.ToDataSourceResult());
                }
            }
                return Json(ModelState.ToDataSourceResult());

        }
        public ActionResult GetTouristAttraction()
        {
            return Json(_dictionaryService.GetTouristAttractionsSchedule());
        }

        public IActionResult AddCounty(int id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
                DictionaryCountyTypeModel dictionaryCounty = _dictionaryService.getCountyIdUpdate(Convert.ToInt32(id));
                return View(dictionaryCounty);
            }
        }
        
        [HttpPost]
        public IActionResult NewCounty(DictionaryCountyTypeModel county, string submitButton)
        {
            if (submitButton == "Cancel")

            {

                return View("County");

            }
            if (county != null)
            {
                string errorMessageForInsertCity;
                if (county.Id == 0)
                {                  
                        errorMessageForInsertCity = _dictionaryService.InsertNewCounty(county);
                        if (String.IsNullOrWhiteSpace(errorMessageForInsertCity))
                        {
                            if (submitButton == "SaveAndNew")
                            {
                                ModelState.Clear();
                                return View("AddCounty");
                            }
                            else
                            {
                                return View("County");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("a", errorMessageForInsertCity);
                            return View("AddCounty");
                        }                    
                }
                else
                {                  
                    errorMessageForInsertCity= _dictionaryService.UpdateCounty(county);
                    if (String.IsNullOrWhiteSpace(errorMessageForInsertCity))
                    {
                        return View("County");
                    }
                    else
                    {
                        ModelState.AddModelError("a", errorMessageForInsertCity);
                        return View("County");
                    }
                }
            }
            else
            {
                return Json(ModelState.ToDataSourceResult());
            }
        }      
    }
}




