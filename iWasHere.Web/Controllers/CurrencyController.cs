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

        public IActionResult Add()
        {
            return View();
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
        public void Save(string txtCurCode, string txtCurName)
        {
            DictionaryCurrency dictionaryCurrency = new DictionaryCurrency();
            dictionaryCurrency.DictionaryCurrencyCode = txtCurCode;
            dictionaryCurrency.DictionaryCurrencyName = txtCurName;
            _dictionaryService.AddDictionaryCurrency(dictionaryCurrency);
        }
    }
}