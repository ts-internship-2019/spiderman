using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Service;
using iWasHere.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class ButtonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
namespace iWasHere.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly DictionaryService _dictionaryService;
        public CurrencyController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Currency()
        {
            return View();
        }
        public IActionResult DictionaryCurrency([DataSourceRequest] DataSourceRequest request)
        {
            List<DictionaryCurrencyModel> myList = _dictionaryService.GetDictionaryCurrencyModel(request.Page, request.PageSize);
            var v2 = new DataSourceResult();
            v2.Data = myList;
            v2.Total = _dictionaryService.GetCountDictionaryCurrency();
            return Json(v2);
        }

        //public IActionResult DictionaryCurrency([DataSourceRequest] DataSourceRequest request)
        //{
        //    return Json(GetDictionaryCurrency().ToDataSourceResult(request));
        //}


        //private IEnumerable<DictionaryCurrencyModel> GetDictionaryCurrency()
        //{
        //    DictionaryCurrencyModel dictionaryCurrencyModel = new DictionaryCurrencyModel();
        //    List<DictionaryCurrencyModel> dbDictionaryCurrencyModel = _dictionaryService.GetDictionaryCurrencyModel();

        //    return dbDictionaryCurrencyModel;
        //    List<DictionaryCurrencyModel> da = new List<DictionaryCurrencyModel>();

        //    for (int i = 0; i < dbDictionaryCurrencyModel.Count(); i++)
        //    {
        //        dictionaryCurrencyModel.DictionaryItemId = dbDictionaryCurrencyModel[i].DictionaryItemId;
        //        dictionaryCurrencyModel.DictionaryItemCode = dbDictionaryCurrencyModel[i].DictionaryItemCode;
        //        dictionaryCurrencyModel.DictionaryItemName = dbDictionaryCurrencyModel[i].DictionaryItemName;
        //        da.Add(dictionaryCurrencyModel);
        //    }

        //    return da;
        //}

        //public ActionResult Orders_Read([DataSourceRequest]DataSourceRequest request)
        //{
        //    return Json(GetDictionaryCurrency().ToDataSourceResult(request));
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        public JsonResult GetJsonCurrencyData()
        {
            return Json(_dictionaryService.GetDictionaryCurrencyModel());
        }
    }
}