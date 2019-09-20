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
        public IActionResult Currency()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dictionaryService.GetDictionaryLandmarkTypeModels();



            //public IActionResult ScheduleFiltered([DataSourceRequest]DataSourceRequest request)
            //{
            //    //var jsonVariable = _dictionaryService.GetDictionaryScheduleFiltred().ToDataSourceResult(request);
            //   // return Json(jsonVariable);
            //}
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

        //public ActionResult AddCategory()
        //{
        //    return View();
        //}

        public ActionResult AddCategory(int id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
                DictionaryCategoryTypeModel dictionaryCategory = _dictionaryService.getCategoryIdUpdate(Convert.ToInt32(id));
                return View(dictionaryCategory);
            }
        }

        [HttpGet]
        public JsonResult GetSelectedCategory(DictionaryCategoryTypeModel dataT)
        {
            return Json(new { result = _dictionaryService.GetSelectedCategory(dataT.Id) });
        }

        [HttpPut]
        public JsonResult AddCategoryController(DictionaryCategoryTypeModel dataT)
        {
            if(dataT.Id == 0 || string.IsNullOrEmpty(Convert.ToString(dataT.Id)))
            {
                _dictionaryService.InsertCategoryType(dataT.Name);
            }else
            {
                //throw new Exception("NU FACE NIMIC");
                _dictionaryService.UpdateCategory(dataT);
            }
            
            return Json(new { result = dataT.Name });
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

        //public void DestroyCategory()
        //{
        //    _dictionaryService.DeleteCategory(abc);
        
        //}

        public IActionResult SearchCountyName()
        {
            return View();
        }


        public IActionResult GetCountyByName([DataSourceRequest]DataSourceRequest request, string name)
        {
            return Json(_dictionaryService.GetDictionaryCountyTypeModelsFilter(request.Page, request.PageSize, name));
        }
    }
}