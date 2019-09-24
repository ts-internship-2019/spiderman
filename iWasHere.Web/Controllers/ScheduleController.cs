using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public ScheduleController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

     
        public IActionResult Schedule()
        {
                return View();
        }


        public IActionResult ScheduleData([DataSourceRequest]DataSourceRequest request)
        {
            var jsonVariable = _dictionaryService.GetDictionaryScheduleModels(request.Page, request.PageSize).ToDataSourceResult(request);
            jsonVariable.Total = 2097152;
            return Json(jsonVariable);
        }
    }
}