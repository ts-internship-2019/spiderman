using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Web.Models;
using iWasHere.Domain.Service;
using iWasHere.Domain.DTOs;
using Kendo.Mvc.UI;
using Stripe;
using Kendo.Mvc.Extensions;

namespace iWasHere.Web.Controllers
{
    public class HomeController : Controller
    {//schimbareiri
        private readonly DictionaryService _dictionaryService;
        public HomeController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Currecy()
        {
            return View();
        }
        public IActionResult DictionaryCurrency([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetDictionaryCurrency().ToDataSourceResult(request));
        }

        private IEnumerable<DictionaryCurrencyModel> GetDictionaryCurrency()
        {
            DictionaryCurrencyModel dictionaryCurrencyModel = new DictionaryCurrencyModel();
            List<DictionaryCurrencyModel> dbDictionaryCurrencyModel = _dictionaryService.GetDictionaryCurrencyModel();

            return dbDictionaryCurrencyModel;
            List<DictionaryCurrencyModel> da = new List <DictionaryCurrencyModel>();

            for (int i = 0; i< dbDictionaryCurrencyModel.Count();i++)
            {
                dictionaryCurrencyModel.DictionaryItemId = dbDictionaryCurrencyModel[i].DictionaryItemId;
                dictionaryCurrencyModel.DictionaryItemCode = dbDictionaryCurrencyModel[i].DictionaryItemCode;
                dictionaryCurrencyModel.DictionaryItemName = dbDictionaryCurrencyModel[i].DictionaryItemName;
                da.Add(dictionaryCurrencyModel);
            }

            return da;
        }

        public ActionResult Orders_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(GetDictionaryCurrency().ToDataSourceResult(request));
        }

        //private IEnumerable<DictionaryCurrencyModel> GetDictionaryCurrency()
        //{
        //    //using (DictionaryCurrencyModel dictionaryCurrencyModel = new DictionaryCurrencyModel())
        //    //{
        //    //    var customers = dictionaryCurrencyModel.Customers.ToList();

        //    //    return dictionaryCurrencyModel.Orders.ToList().Select(order => new DictionaryCurrencyModel
        //    //    {
        //    //        ContactName = customers.First(c => c.CustomerID == order.CustomerID).ContactName,
        //    //        Freight = order.Freight,
        //    //        OrderDate = order.OrderDate,
        //    //        ShippedDate = order.ShippedDate,
        //    //        OrderID = order.OrderID,
        //    //        ShipAddress = order.ShipAddress,
        //    //        ShipCountry = order.ShipCountry,
        //    //        ShipName = order.ShipName,
        //    //        ShipCity = order.ShipCity,
        //    //        EmployeeID = order.EmployeeID,
        //    //        CustomerID = order.CustomerID
        //    //    }).ToList();
        //    //}
        //}

        //public ActionResult Products_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    return Json(ProductService.Read().ToDataSourceResult(request));
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //public IActionResult Currency()
        //{
        //    return View(new DictionaryCurrencyModel { DictionaryItemId = Activity.Current ?? HttpContext.TraceIdentifier });
        //}
    }

}
