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
    public class DictionaryService
    {
        private readonly DatabaseContext _dbContext;
        public DictionaryService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypeModels()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.DictionaryItemId,
                Name = a.DictionaryItemName
            }).ToList();

            return dictionaryLandmarkTypeModels;
        }

        public List<DictionaryCountyTypeModel> GetDictionaryCountyTypeModels(int page, int pageSize)
        {
            //pageSize = 10;
            int skip = (page - 1) * pageSize;
            List<DictionaryCountyTypeModel> dictionaryCountyTypeModel = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyTypeModel()
            {
                Id = a.DictionaryCountyId,
                Name = a.DictionaryCountyName,
                CountryName = a.DictionaryCountry.DictionaryCountryName
            }).Skip(skip).Take(pageSize).ToList();

            return dictionaryCountyTypeModel;
        }

        public List<CountryListModel> GetAllCountries()
        {
            List<CountryListModel> countryListModels = _dbContext.DictionaryCountry.Select(a => new CountryListModel()
            {
                Id = a.DictionaryCountryId,
                Name = a.DictionaryCountryName
            }).ToList();

            return countryListModels;

        }

        public List<DictionaryCountyTypeModel> GetDictionaryCountyTypeModelsFilter(int page, int pageSize, string name)
        {
            //pageSize = 10;
            int skip = (page - 1) * pageSize;
            List<DictionaryCountyTypeModel> dictionaryCountyTypeModel =
                _dbContext.DictionaryCounty
                .Where(a => a.DictionaryCountyName == name)
                .Select(a => new DictionaryCountyTypeModel()
                {
                    Id = a.DictionaryCountyId,
                    Name = a.DictionaryCountyName,
                    CountryName = a.DictionaryCountry.DictionaryCountryName
                }).Skip(skip).Take(pageSize).ToList();


            //comentariu
            return dictionaryCountyTypeModel;
        }

        public List<DictionaryCategoryTypeModel> GetDictionaryCategoryTypeModel(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            List<DictionaryCategoryTypeModel> dictionaryCategoryTypeModels = _dbContext.DictionaryCategory.Select(a => new DictionaryCategoryTypeModel()
            {
                Id = a.DictionaryCategoryId,
                Name = a.DictionaryCategoryName,

            }).Skip(skip).Take(pageSize).ToList();

            return dictionaryCategoryTypeModels;
        }

        public List<DictionaryCategory> GetSelectedCategory(int id)
        {
            var categoryName = _dbContext.DictionaryCategory.Where(categ => categ.DictionaryCategoryId == id);
            List<DictionaryCategory> categoryListModel = categoryName.Take(1).ToList();
            return categoryListModel;
        }

        public List<DictionaryCategoryTypeModel> GetCategoryData(string text)
        {
            var categoryName = _dbContext.DictionaryCategory.Select(categ => new DictionaryCategoryTypeModel()
            {
                Id = categ.DictionaryCategoryId,
                Name = categ.DictionaryCategoryName
            });
            if (!string.IsNullOrEmpty(text))
            {
                categoryName = categoryName.Where(c => c.Name.StartsWith(text));
            }
            List<DictionaryCategoryTypeModel> categoryListModel = categoryName.Take(50).ToList();
            return categoryListModel;
        }

        public string DeleteCategory(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryCategory.Single(a => a.DictionaryCategoryId.Equals(id)));
                _dbContext.SaveChanges();
                return null;
            }
            catch
            {
                return "Cannot delete category!";
            }
        }

        public List<DictionaryCategoryTypeModel> GetDictionaryCategoryTypeFilter(int page, int pageSize, string name)
        {
            //pageSize = 10;
            int skip = (page - 1) * pageSize;
            
            List<DictionaryCategoryTypeModel> dictionaryCityyTypeModel =
                _dbContext.DictionaryCategory
                .Where(a => a.DictionaryCategoryName.StartsWith(name))
                .Select(a => new DictionaryCategoryTypeModel()
                {
                    Id = a.DictionaryCategoryId,
                    Name = a.DictionaryCategoryName
                }).Skip(skip).Take(pageSize).ToList();

            return dictionaryCityyTypeModel;
        }

        public List<DictionaryCategoryTypeModel> TestCategory(int page, int pageSize, string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return GetDictionaryCategoryTypeModel(page,pageSize);
            }
            else
            {
                return GetDictionaryCategoryTypeFilter(page, pageSize, categoryName);
            }
        }

        public void InsertCategoryType(string name)
        {
            DictionaryCategory dictionary = new DictionaryCategory();
            dictionary.DictionaryCategoryName = name;

            _dbContext.DictionaryCategory.Add(dictionary);
            _dbContext.SaveChanges();
        }
        public int FilterTotalCategory(string name)
        {
            int i = _dbContext.DictionaryCategory.Where(a => a.DictionaryCategoryName.StartsWith(name)).Count();
            return i;
        }

        public int Total()
        {
            int i = _dbContext.DictionaryCategory.Count();
            return i;
        }
        public List<DictionaryCityModel> GetDictionaryCityModels(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            List<DictionaryCityModel> dictionaryCityModels = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
            {
                Id = a.DictionaryCityId,
                CityName = a.DictionaryCityName,
                CountyName = a.DictionaryCounty.DictionaryCountyName,
                CountryName = a.DictionaryCounty.DictionaryCountry.DictionaryCountryName
            }).Skip(skip).Take(pageSize).ToList();

            return dictionaryCityModels;


        }
        public List<DictionaryCountyModel> GetDictionaryCountiesCB()
        {
            List<DictionaryCountyModel> dictionaryLCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.DictionaryCountyId,
                CountyName = a.DictionaryCountyName

            }).ToList();

            return dictionaryLCounties;
        }
        public List<DictionaryCountryModel> GetDictionaryCountriesCB()
        {
            List<DictionaryCountryModel> dictionaryLCountries = _dbContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
            {
                CountryId = a.DictionaryCountryId,
                CountryName = a.DictionaryCountryName

            }).ToList();

            return dictionaryLCountries;
        }
        public int TotalCity()
        {
            int i = _dbContext.DictionaryCity.Count();
            return i;
        }



        public int TestTotal(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return Total();
            }
            else
            {
                return FilterTotalCategory(categoryName);
            }
        }

        public List<ScheduleTouristAttractionModel> GetDictionaryScheduleModels(int page, int pageSize)
        {

            int pageSkip = (page - 1) * pageSize;
            List<ScheduleTouristAttractionModel> scheduleTouristAttractions = _dbContext.Schedule.
            Select(a => new ScheduleTouristAttractionModel()
            {

                Day = a.Day,
                StartHour = a.StartHour,
                EndHour = a.EndHour,
                Season = a.Season.DictionarySeasonName,
                TouristAttractionName = a.TouristAttraction.Name



            }).Skip(pageSkip).Take(pageSize).ToList();

            return scheduleTouristAttractions;

        }


        public IQueryable<ScheduleFilteredModel> GetDictionaryScheduleFiltred(string searchString)
        {
            IQueryable<ScheduleFilteredModel> schedulefiltered = _dbContext.Schedule.Where(s => s.TouristAttraction.Name.Contains(searchString)).Select(a => new ScheduleFilteredModel());


            return schedulefiltered;



        }

        public int GetItemsOfSchedule()
        {
            return _dbContext.Schedule.Count();
        }

        public DictionaryCategoryTypeModel getCategoryIdUpdate(int id)
        {
            DictionaryCategoryTypeModel dictionaryCountyTypeModel = _dbContext.DictionaryCategory.Where(a => a.DictionaryCategoryId == id)
                .Select(a => new DictionaryCategoryTypeModel()
                {
                    Id = a.DictionaryCategoryId,
                    Name = a.DictionaryCategoryName,
                }).First();
            return dictionaryCountyTypeModel;
        }

        public string UpdateCategory(DictionaryCategoryTypeModel dictionaryCategoryTypeModel)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(dictionaryCategoryTypeModel.Name))
                {
                    return "Categoria trebuie completata trebuie completat";
                }
                else
                {
                    DictionaryCategory dictionaryCategory = _dbContext.DictionaryCategory.Find(dictionaryCategoryTypeModel.Id);
                    dictionaryCategory.DictionaryCategoryName = dictionaryCategoryTypeModel.Name;
                    //dictionaryCategory.DictionaryCategoryId = dictionaryCategoryTypeModel.DictionaryCategoryId;
                   
                    _dbContext.DictionaryCategory.Update(dictionaryCategory);
                    _dbContext.SaveChanges();

                    return null;
                }
            }
            catch (Exception e)
            {
                return "Trebuie sa completati campurile";
            }
        }
    }
}
