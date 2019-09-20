using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
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

        //public IActionResult Add()
        //{
        //    return View();
        //}
        public IActionResult Add(int id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
                DictionaryCurrency dictionaryCurrency = _dictionaryService.GetCurrency(id);
                DictionaryCurrencyModel dcm = new DictionaryCurrencyModel();
                dcm.DictionaryItemCode = dictionaryCurrency.DictionaryCurrencyCode;
                dcm.DictionaryItemName = dictionaryCurrency.DictionaryCurrencyName;
                return View(dcm);
            }
        }
        public IActionResult Currency()
        {
            return View();
        }
        public IActionResult DictionaryCurrency([DataSourceRequest] DataSourceRequest request, string txtFilterName)
        {
            List<DictionaryCurrencyModel> myList = _dictionaryService.GetDictionaryCurrencyModel(request.Page, request.PageSize, txtFilterName);
            var v2 = new DataSourceResult();
            v2.Data = myList;
            v2.Total = _dictionaryService.GetCountDictionaryCurrency(txtFilterName);
            return Json(v2);
        }
        public JsonResult GetJsonCurrencyData()
        {
            return Json(_dictionaryService.GetDictionaryCurrencyModel());
        }
        public ActionResult Save([DataSourceRequest] DataSourceRequest request, DictionaryCurrencyModel dict, string submitValue)
        {
            if (submitValue == "save")
            {
                DictionaryCurrency dictionaryCurrency = new DictionaryCurrency();
                dictionaryCurrency.DictionaryCurrencyCode = dict.DictionaryItemCode;
                dictionaryCurrency.DictionaryCurrencyName = dict.DictionaryItemName;
                _dictionaryService.AddDictionaryCurrency(dictionaryCurrency);
                return View("Index");
            }
            else if (submitValue == "cancel")
            {
                return View("Index");
            }
            else
            {
                DictionaryCurrency dictionaryCurrency = new DictionaryCurrency();
                dictionaryCurrency.DictionaryCurrencyCode = dict.DictionaryItemCode;
                dictionaryCurrency.DictionaryCurrencyName = dict.DictionaryItemName;
                _dictionaryService.AddDictionaryCurrency(dictionaryCurrency);
                return View("Add");
            }
        }
        public ActionResult SaveAndNew([DataSourceRequest] DataSourceRequest request, DictionaryCurrencyModel dict)
        {
            DictionaryCurrency dictionaryCurrency = new DictionaryCurrency();
            dictionaryCurrency.DictionaryCurrencyCode = dict.DictionaryItemCode;
            dictionaryCurrency.DictionaryCurrencyName = dict.DictionaryItemName;
            _dictionaryService.AddDictionaryCurrency(dictionaryCurrency);
            return View("Add");
        }


        public ActionResult DestroyCurrency([DataSourceRequest] DataSourceRequest request, DictionaryCurrencyModel currency)
        {
            if (currency != null)
            {
                string errorMessage = _dictionaryService.DeleteCurrency(currency.DictionaryItemId);
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    ModelState.AddModelError("a", errorMessage);
                }
            }


            return Json(ModelState.ToDataSourceResult());
        }



        public ActionResult UpdateCurrency([DataSourceRequest] DataSourceRequest request, DictionaryCurrencyModel currency)
        {
            //TO DO
            //if (currency != null)
            //{
            //    string errorMessage = _dictionaryService.DeleteCurrency(currency.DictionaryItemId);
            //    if (!string.IsNullOrWhiteSpace(errorMessage))
            //    {
            //        ModelState.AddModelError("a", errorMessage);
            //    }
            //}
            return Json(ModelState.ToDataSourceResult());
        }

    }
}