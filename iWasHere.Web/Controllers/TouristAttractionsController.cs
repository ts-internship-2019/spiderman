using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using iWasHere.Domain.DTOs;
using Microsoft.AspNetCore.Hosting;
using iWasHere.Domain.Service;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace iWasHere.Controllers
{
    public class TouristAttractionsController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
     
        private readonly DictionaryService _dictionaryService;
        private readonly DatabaseContext _context;

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TouristAttractions()
        {
            return View();
        }
        public TouristAttractionsController(DatabaseContext context,IHostingEnvironment hostingEnvironmentt,DictionaryService service)
        {
            hostingEnvironment = hostingEnvironmentt;
            _context = context;
            _dictionaryService = service;
        }

      
        public IActionResult TouristAttractionsGrid([DataSourceRequest] DataSourceRequest request, string txtFilterName)
        {
            List<TouristAttraction> myList = _dictionaryService.GetTouristAttractionsModel(request.Page, request.PageSize, txtFilterName);
            var v2 = new DataSourceResult();
            v2.Data = myList;
            v2.Total = _dictionaryService.GetCountTouristAttraction(txtFilterName);
            return Json(v2);
        }


        public ActionResult DestroyTouristAttractions([DataSourceRequest] DataSourceRequest request, DictionaryCurrencyModel currency)
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


        public ActionResult UpdateTouristAttractions([DataSourceRequest] DataSourceRequest request, DictionaryCurrencyModel currency)
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
        public IActionResult CreateEdit(int Id)
        {
            if (Convert.ToInt32(Id) == 0)
            {
                return View();
            }
            else
            {
                TouristAttraction touristAttraction = _dictionaryService.GetTouristAttractions(Id);
                touristAttraction.TouristAttractionId = Id;
                touristAttraction.Category = _dictionaryService.GetDictionaryCategory(touristAttraction.CategoryId);
                touristAttraction.City = _dictionaryService.GetDictionaryCity(touristAttraction.CityId);
                touristAttraction.Landmark = _dictionaryService.GetLandmark(touristAttraction.LandmarkId);
                return View(touristAttraction);
                //return View();
            }
        }
        public ActionResult GetCategory()
        {
            return Json(_dictionaryService.GetTouristAttractionsCategory());
        }
        public ActionResult GetSchedule()
        {
            return Json(_dictionaryService.GetTouristAttractionsSchedule());
        }
        public ActionResult GetCity(string txtName)
        {
            return Json(_dictionaryService.GetTouristAttractionsCity(txtName));
        }
        [HttpPost]
        public ActionResult Save([DataSourceRequest] DataSourceRequest request, TouristAttractionScheduleModel tA, string submitValue, IFormFile[] photos)
        {
            if (tA.TouristAttractionId <= 0)
            {
                if (submitValue == "save")
                {
                    TouristAttraction touristAttraction = new TouristAttraction();
                    touristAttraction.TouristAttractionId = tA.TouristAttractionId;
                    touristAttraction.Name = tA.Name;
                    touristAttraction.Description = tA.Description;
                    touristAttraction.Longtitudine = tA.Longtitudine;
                    touristAttraction.Latitudine = tA.Latitudine;
                    touristAttraction.CityId = tA.CityId;
                    touristAttraction.LandmarkId = tA.LandmarkId;
                    touristAttraction.CategoryId = tA.CategoryId;
                    touristAttraction.City = _dictionaryService.GetDictionaryCity(tA.CityId);
                    touristAttraction.Category = _dictionaryService.GetDictionaryCategory(tA.CategoryId);
                    touristAttraction.Landmark = _dictionaryService.GetLandmark(tA.LandmarkId);
                    touristAttraction.CityId = 1;
                    touristAttraction.CategoryId = 1;
                    touristAttraction.LandmarkId = 4;

                    int touristAttractionId = _dictionaryService.AddTouristAttractions(touristAttraction);
                    AddImg(photos, touristAttractionId);
                    return View("Index");
                }
                else if (submitValue == "cancel")
                {
                    return View("Index");
                }
                else
                {
                    TouristAttraction touristAttraction = new TouristAttraction();
                    touristAttraction.TouristAttractionId = tA.TouristAttractionId;
                    touristAttraction.Name = tA.Name;
                    touristAttraction.Description = tA.Description;
                    touristAttraction.Longtitudine = tA.Longtitudine;
                    touristAttraction.Latitudine = tA.Latitudine;
                    touristAttraction.CityId = tA.CityId;
                    touristAttraction.LandmarkId = tA.LandmarkId;
                    touristAttraction.CategoryId = tA.CategoryId;
                    touristAttraction.City = _dictionaryService.GetDictionaryCity(tA.CityId);
                    touristAttraction.Category = _dictionaryService.GetDictionaryCategory(tA.CategoryId);
                    touristAttraction.Landmark = _dictionaryService.GetLandmark(tA.LandmarkId);
                    touristAttraction.CityId = 1;
                    touristAttraction.CategoryId = 1;
                    touristAttraction.LandmarkId = 4;
                    int touristAttractionId=_dictionaryService.AddTouristAttractions(touristAttraction);
                    AddImg(photos,touristAttractionId);
                    ModelState.Clear();
                    return View("CreateEdit");
                }
            }
            else
            {
                if (submitValue == "save")
                {
                    TouristAttraction touristAttraction = new TouristAttraction();
                    touristAttraction.TouristAttractionId = tA.TouristAttractionId;
                    touristAttraction.Name = tA.Name;
                    touristAttraction.Description = tA.Description;
                    touristAttraction.Longtitudine = tA.Longtitudine;
                    touristAttraction.Latitudine = tA.Latitudine;
                    touristAttraction.CityId = tA.CityId;
                    touristAttraction.LandmarkId = tA.LandmarkId;
                    touristAttraction.CategoryId = tA.CategoryId;
                    touristAttraction.City = _dictionaryService.GetDictionaryCity(tA.CityId);
                    touristAttraction.Category = _dictionaryService.GetDictionaryCategory(tA.CategoryId);
                    touristAttraction.Landmark = _dictionaryService.GetLandmark(tA.LandmarkId);

                    _dictionaryService.EditTouristAttractions(touristAttraction);
                    return View("Index");
                }
                else
                {
                    return View("Index");
                }
            }

        }
        //[HttpGet]
        //public ActionResult Image()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Image(ImageModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string uniqueFileName = null;

        //        // If the Photo property on the incoming model object is not null, then the user
        //        // has selected an image to upload.
        //        if (model.Photo != null)
        //        {
        //            // The image must be uploaded to the images folder in wwwroot
        //            // To get the path of the wwwroot folder we are using the inject
        //            // HostingEnvironment service provided by ASP.NET Core
        //            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
        //            // To make sure the file name is unique we are appending a new
        //            // GUID value and and an underscore to the file name
        //            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
        //            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //            // Use CopyTo() method provided by IFormFile interface to
        //            // copy the file to wwwroot/images folder
        //            model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
        //        }

        //        Image img = new Image
        //        {
        //         TouristAttractionId=2,
        //            Path = uniqueFileName
        //        };

        //        _dictionaryService.AddImage(img);
                
        //    }

        //    return View();
        //}
        [HttpGet]
        public ActionResult AddImages()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult AddImages(IFormFile[] photos)
        {
          
          
                foreach (IFormFile photo in photos)
                {
                    string uniqueFileName = null;
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                 
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                   photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    

                    Image img = new Image
                    {
                        TouristAttractionId = 2,
                        Path = uniqueFileName
                    };
                    _dictionaryService.AddImage(img);
                }
            
           
            return View();
        }
      
         public void AddImg(IFormFile[] photos,int id)
        {


            foreach (IFormFile photo in photos)
            {
                string uniqueFileName = null;
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                photo.CopyTo(new FileStream(filePath, FileMode.Create));


                Image img = new Image
                {
                    TouristAttractionId = id,
                    Path = uniqueFileName
                };
                _dictionaryService.AddImage(img);
            }


          
        }
    }
    
}
