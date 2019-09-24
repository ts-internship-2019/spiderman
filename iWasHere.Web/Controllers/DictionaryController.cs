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
        
       
        public ActionResult NewCity(DictionaryCityModel model, string submit)
        {
            if (submit == "cancel")
            {
                return View("City");
            }

            if (model.CityName ==""||String.IsNullOrEmpty(model.CityName)) {
                ModelState.AddModelError("a", "Trebuie sa inserati orasul");
                return View("AddCity");

            }
            if (model.CountyId == 0 )
            {
                ModelState.AddModelError("a", "Trebuie sa inserati judetul");
                return View("AddCity");

            }
            if (model.CityId <= 0)
            {
                _dictionaryService.InsertCity(model);
            }
            else
            {
                _dictionaryService.UpdateCity(model);
            }
            if (submit == "savenew")
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
            return View();
        }

        public ActionResult Search(string searchText)
        {
            return Json(searchText);
        }

        public void CategoryComboBox_Read(string text,string abc)
        {
            if (!string.IsNullOrEmpty(abc))
            {
                _dictionaryService.InsertCategoryType(abc);
            }

            //return Json(_dictionaryService.GetCategoryData(text));
        }

        public ActionResult DestroyCategory(DictionaryCategoryTypeModel category)
        {
            if (!string.IsNullOrEmpty(category.Id.ToString()))
            {
                string errorMessage = _dictionaryService.DeleteCategory(category.Id);
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    ModelState.AddModelError("A", errorMessage);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult CategoryBinding_Read([DataSourceRequest] DataSourceRequest request, string categoryName)
        {
            var jsonVariable = _dictionaryService.TestCategory(request.Page, request.PageSize, categoryName);

            DataSourceResult result = new DataSourceResult()
            {
                Data = jsonVariable,
                Total = _dictionaryService.TestTotal(categoryName)
            };

            return Json(result);


            //if (string.IsNullOrEmpty(categoryName))
            //{
            //    var jsonVariable = _dictionaryService.GetDictionaryCategoryTypeModel(request.Page, request.PageSize);

            //    DataSourceResult result = new DataSourceResult()
            //    {
            //        Data = jsonVariable,
            //        Total = _dictionaryService.Total()
            //    };

            //    return Json(result);
            //}
            //else
            //{
            //    var jsonVariable = _dictionaryService.GetDictionaryCategoryTypeFilter(request.Page, request.PageSize, categoryName);

            //    DataSourceResult result = new DataSourceResult()
            //    {
            //        Data = jsonVariable,
            //        Total = _dictionaryService.FilterTotalCategory(categoryName)
            //    };

            //    return Json(result);
            //}

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

        public ActionResult FilterGetCounties(string text)
        {
            return Json(_dictionaryService.Filter_GetCounties(text));
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
        public ActionResult DeleteCityData([DataSourceRequest] DataSourceRequest request, DictionaryCityModel model)
        {
            if (model!= null)
            {
                string errorMessage = _dictionaryService.DeleteCity(model.CityId);
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
            int rows;
            var x = _dictionaryService.GetCountryModel(request.Page, request.PageSize, out rows, abc);
            DataSourceResult dataSource = new DataSourceResult
            {
                Data = x,
                Total = rows
            };

            return Json(dataSource);
        }

        public ActionResult Process_Destroy([DataSourceRequest] DataSourceRequest request, ScheduleTouristAttractionModel schedule)
        {
            if (schedule != null)
            {
                string errorMessageForClient = _dictionaryService.DeleteSchedule(schedule.ScheduleId);
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

        public ActionResult DestroyCountry([DataSourceRequest] DataSourceRequest request, DictionaryCountryModel country)
        {
            if (country != null)
            {
                string errorMessage =_dictionaryService.DeleteCountry(country.CountryId);
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    ModelState.AddModelError("a", errorMessage);
                    return Json(ModelState.ToDataSourceResult());
                }
                else {
                    return Json(ModelState.ToDataSourceResult());
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }
        //public IActionResult AddEditCountry()
        //{
        //    return View();
        //}

        //public ActionResult AddEditCountry(DictionaryCountry cm)
        //{
        //    if (ModelState.IsValid && cm != null)
        //    {
        //        _dictionaryService.AddDictionaryCountry(cm);
        //    }
        //    return View();
        //}

        public IActionResult AddEditCountry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditCountry(DictionaryCountryModel c)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return Content($"{c.CountryCode}, {c.CountryName}");
        }
    


    }
}