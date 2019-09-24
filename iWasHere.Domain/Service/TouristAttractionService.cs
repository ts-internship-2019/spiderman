using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace iWasHere.Domain.Service
{
    public class TouristAttractionService
    {
        private readonly DatabaseContext _dbContext;

        public TouristAttractionService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<TouristAttractionModel> GetObjectiveDetails(int id)
        {
            //TouristAttraction touristAttraction = _dbContext.TouristAttraction.Find(id);
            //touristAttraction.City = _dbContext.DictionaryCity.Find(touristAttraction.CityId);
            //touristAttraction.Category = _dbContext.DictionaryCategory.Find(touristAttraction.CategoryId);
            //touristAttraction.Landmark = _dbContext.DictionaryLandmarkType.Find(touristAttraction.LandmarkId);


            var categoryName = _dbContext.TouristAttraction
                .Include(a => a.City)
                .Include(a => a.Category)
                .Include(a=> a.Landmark)
                .Include(a => a.Image)
                .Where(categ => categ.TouristAttractionId == id)
                .Select(a=> new TouristAttractionModel() {
                    CategoryName = a.Category.DictionaryCategoryName,
                    CityName = a.City.DictionaryCityName,
                    LandmarkTypeName = a.Landmark.DictionaryItemName,
                    Latitudine = a.Latitudine,
                    Longtitudine = a.Longtitudine,
                    Description = a.Description,
                    Image = a.Image.Select(b => b.Path),
                    Name = a.Name
                });
            List<TouristAttractionModel> categoryListModel = categoryName.Take(1).ToList();
                
            return categoryListModel;
                
            //return touristAttraction;
        }
    }
}
