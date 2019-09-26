using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using iWasHere.Domain.DTOs;
using Microsoft.AspNetCore.Hosting;
using iWasHere.Domain.Service;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Reflection.Metadata;

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
        public TouristAttractionsController(DatabaseContext context, IHostingEnvironment hostingEnvironmentt, DictionaryService service)
        {
            hostingEnvironment = hostingEnvironmentt;
            _context = context;
            _dictionaryService = service;
        }


        public IActionResult TouristAttractionsGrid([DataSourceRequest] DataSourceRequest request, string txtFilterName)
        {
            List<TouristAttractionsDTO> myList = _dictionaryService.GetTouristAttractionsModel(request.Page, request.PageSize, txtFilterName);
            DataSourceResult v2 = new DataSourceResult();
            v2.Data = myList;
            v2.Total = _dictionaryService.GetCountTouristAttraction(txtFilterName);
            return Json(v2);
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

                TouristAttractionsDTOEdit touristAttractionsDTOEdit = new TouristAttractionsDTOEdit();
                touristAttractionsDTOEdit.Name = touristAttraction.Name;
                touristAttractionsDTOEdit.Description = touristAttraction.Description;
                touristAttractionsDTOEdit.Latitudine = touristAttraction.Latitudine;
                touristAttractionsDTOEdit.Longtitudine = touristAttraction.Longtitudine;
                touristAttractionsDTOEdit.CityId = touristAttraction.CityId;
                touristAttractionsDTOEdit.CategoryId = touristAttraction.CategoryId;
                touristAttractionsDTOEdit.LandmarkId = touristAttraction.LandmarkId;
                touristAttractionsDTOEdit.TouristAttractionId = Id;
                ViewData["Images"] = _dictionaryService.GetImages(Id);
                return View(touristAttractionsDTOEdit);
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
        public ActionResult Save([DataSourceRequest] DataSourceRequest request, TouristAttractionsDTOEdit tA, string submitValue, IFormFile[] photos)
        {
            if (tA.TouristAttractionId <= 0)
            {
                if (submitValue == "save")
                {
                    string errorMessage = "";
                    string errorMessageNumber = "";
                    string errorMessageLength = "";
                    bool statusError = false;
                    TouristAttraction touristAttraction = new TouristAttraction();
                    if (String.IsNullOrEmpty(tA.Name) || string.IsNullOrWhiteSpace(tA.Name))
                    {
                        errorMessage += "\"Nume\"";
                        statusError = true;
                    }
                    else if (!(tA.Name.Length < 22))
                    {
                        errorMessageLength += "\"Nume(22)\"";
                        statusError = true;
                    }
                    else
                        touristAttraction.Name = tA.Name;
                    if (String.IsNullOrEmpty(tA.Description) || string.IsNullOrWhiteSpace(tA.Description))
                    {
                        errorMessage += "\"Descriere\"";
                        statusError = true;
                    }
                    else if (!(tA.Description.Length < 220))
                    {
                        errorMessageLength += "\"Descriere(220)\"";
                        statusError = true;
                    }
                    else
                        touristAttraction.Description = tA.Description;
                    if (tA.CategoryId == 0)
                    {
                        errorMessage += "\"Categorie\"";
                        statusError = true;
                    }
                    else
                        touristAttraction.CategoryId = tA.CategoryId;
                    if (tA.LandmarkId == 0)
                    {
                        errorMessage += "\"Landmark\"";
                        statusError = true;
                    }
                    else
                        touristAttraction.LandmarkId = tA.LandmarkId;
                    if (tA.CityId == 0)
                    {
                        errorMessage += "\"Oras\"";
                        statusError = true;
                    }
                    else
                        touristAttraction.CityId = tA.CityId;

                    if (String.IsNullOrEmpty(tA.Longtitudine) || string.IsNullOrWhiteSpace(tA.Longtitudine))
                    {
                        errorMessage += "\"Longitudine\"";
                        statusError = true;
                    }
                    else if (!float.TryParse(tA.Longtitudine, out _))
                    {
                        errorMessageNumber += "\"Longitudine\"";
                        statusError = true;
                    }
                    else if (!(tA.Longtitudine.Length < 22))
                    {
                        errorMessageLength += "\"Longitutine(22)\"";
                        statusError = true;
                    }
                    else
                        touristAttraction.Longtitudine = tA.Longtitudine;

                    if (String.IsNullOrEmpty(tA.Latitudine) || string.IsNullOrWhiteSpace(tA.Latitudine))
                    {
                        errorMessage += "\"Latitudine\"";
                        statusError = true;
                    }
                    else if (!float.TryParse(tA.Latitudine, out _))
                    {
                        errorMessageNumber += "\"Latitudine\"";
                        statusError = true;
                    }
                    else if (!(tA.Latitudine.Length < 22))
                    {
                        errorMessageLength += "\"Latitudine(22)\"";
                        statusError = true;
                    }
                    else
                        touristAttraction.Latitudine = tA.Latitudine;

                    if (statusError == true)
                    {
                        if (!errorMessageLength.Equals(""))
                        {
                            errorMessageLength = errorMessageLength.Replace("\"\"", "\", \"");
                            errorMessageLength = "Va rugam respectati dimensiunea la " + errorMessageLength + ".";
                        }
                        if (!errorMessage.Equals(""))
                        {
                            errorMessage = errorMessage.Replace("\"\"", "\", \"");
                            errorMessage = "Va rugam completati " + errorMessage + ".";
                        }
                        if (!errorMessageNumber.Equals(""))
                        {
                            errorMessageNumber = errorMessageNumber.Replace("\"\"", "\", \"");
                            errorMessageNumber = "Va rugam completati cu valori numerice " + errorMessageNumber + ".";
                        }
                        ModelState.AddModelError(string.Empty, errorMessage);
                        ModelState.AddModelError(string.Empty, errorMessageNumber);
                        ModelState.AddModelError(string.Empty, errorMessageLength);
                        return View("CreateEdit");
                    }

                    touristAttraction.TouristAttractionId = tA.TouristAttractionId;
                    //touristAttraction.Description = tA.Description;
                    //touristAttraction.Longtitudine = tA.Longtitudine;
                    //touristAttraction.Latitudine = tA.Latitudine;
                    touristAttraction.CityId = tA.CityId;
                    touristAttraction.LandmarkId = tA.LandmarkId;
                    touristAttraction.CategoryId = tA.CategoryId;

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

                    int touristAttractionId = _dictionaryService.AddTouristAttractions(touristAttraction);
                    AddImg(photos, touristAttractionId);
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

                    _dictionaryService.EditTouristAttractions(touristAttraction);
                    AddImg(photos, touristAttraction.TouristAttractionId);
                    return View("Index");
                }
                else
                {
                    return View("Index");
                }
            }

        }
        public bool IsNumeric(string input)
        {
            return int.TryParse(input, out int test);
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
        public ActionResult GetLandmark(string txtName)
        {
            return Json(_dictionaryService.GetTouristAttractionsLandmark_v2());
        }
        public ActionResult DestroyTouristAttractions([DataSourceRequest] DataSourceRequest request, TouristAttractionsDTO tA)
        {
            if (tA != null)
            {
                string errorMessage = _dictionaryService.DeleteTouristAttraction(tA.TouristAttractionId);
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    ModelState.AddModelError("a", errorMessage);
                }
            }


            return Json(ModelState.ToDataSourceResult());
        }
        public IActionResult TouristAttractionsByCountry(int Id) {
            TouristAttractionsDTO dto = new TouristAttractionsDTO();
            dto.CountryId = Id;
           // ViewData["CountryId"] = Id;
            return View(dto);
        }
        public IActionResult TouristAttractionsByCountryData([DataSourceRequest] DataSourceRequest request, int idcountry)
        {
            List<TouristAttractionsDTO> myList = _dictionaryService.GetTouristAttractionsByCountry(idcountry,request.Page, request.PageSize);
            DataSourceResult v2 = new DataSourceResult();
            v2.Data = myList;
            v2.Total = _dictionaryService.TouristAttractionsbyCountryCount(idcountry);
            return Json(v2);
        }

        public IActionResult SaveDataInWord(int Id)
        {
            TouristAttraction touristAttraction = _dictionaryService.GetTouristAttractions(Id);
            touristAttraction.City.DictionaryCounty = _dictionaryService.GetCounty(touristAttraction.City.DictionaryCountyId);

            Stream stream = _dictionaryService.SaveDataInWord(touristAttraction);

            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "TouristAttraction.docx");
        }
    }

}
