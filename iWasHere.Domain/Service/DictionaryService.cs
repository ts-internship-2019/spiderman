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
   
        public void InsertCategoryType(string name)
        {
            DictionaryCategory dictionary = new DictionaryCategory();
            dictionary.DictionaryCategoryName = name;

            _dbContext.DictionaryCategory.Add(dictionary);
            _dbContext.SaveChanges();

        }
        public int FilterTotalCategory(string name)
        {
            int i = _dbContext.DictionaryCategory.Where(a => a.DictionaryCategoryName == name).Count();
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

        //Andrei
        public int GetItemsOfSchedule()
        {
            return _dbContext.Schedule.Count();
        }


        public List<DictionaryCountryModel> GetDictionaryCountryModels()
        {
            List<DictionaryCountryModel> dictionaryCountryModels = _dbContext.DictionaryCountry.Select(DictionaryCountry => new DictionaryCountryModel()
            {
                CountryId = DictionaryCountry.DictionaryCountryId,
                CountryName = DictionaryCountry.DictionaryCountryName
            }).ToList();

            return dictionaryCountryModels;
        }
        /*
        public List<DictionaryCountryModel> GetDictionaryCountryModel(int page, int pageSize)
        {

            int skip = (page - 1) * pageSize;
            List<DictionaryCountryModel> countries = _dbContext.DictionaryCountry.
            Select(a => new DictionaryCountryModel()
            {
                CountryId = a.DictionaryCountryId,
                CountryName = a.DictionaryCountryName
            }).Skip(skip).Take(pageSize).ToList();

            return countries;

        }
        */

        public int TotalCountries()
        {
            int i = _dbContext.DictionaryCountry.Count();
            return i;
        }


        public IEnumerable<DictionaryCountryModel> GetCountryModel(int page, int pageSize, out int rows, string abc)
        {
            rows = 0;
            rows = _dbContext.DictionaryCountry.Count();
            if (abc == null)
                return _dbContext.DictionaryCountry.Skip((page - 1) * pageSize).Take(pageSize).Select(Country => new DictionaryCountryModel
                {
                    CountryId = Country.DictionaryCountryId,
                    CountryName = Country.DictionaryCountryName
                });
            else
            {
                rows = _dbContext.DictionaryCountry.Where(a => a.DictionaryCountryName.StartsWith(abc)).Count();
                return _dbContext.DictionaryCountry.Where(a => a.DictionaryCountryName.StartsWith(abc)).Skip((page - 1) * pageSize).Take(pageSize).Select(Country => new DictionaryCountryModel
                {
                    CountryId = Country.DictionaryCountryId,
                    CountryName = Country.DictionaryCountryName
                });
            }
        }

        /*
        public List<DictionaryCountryModel> GetDictionaryCountryFilter(int page, int pageSize, string name)
        {
            int skip = (page - 1) * pageSize;
            List<DictionaryCountryModel> countryFilter =
                _dbContext.DictionaryCountry
                .Where(a => a.DictionaryCountryName == name || a.DictionaryCountryName.StartsWith(name))
                .Select(a => new DictionaryCountryModel()
                {
                    CountryId = a.DictionaryCountryId,
                    CountryName = a.DictionaryCountryName
                }).Skip(skip).Take(pageSize).ToList();

            return countryFilter;
        }
        */

        public int FilterTotalCountries(string name)
        {
            int i = _dbContext.DictionaryCountry.Where(a => a.DictionaryCountryName == name).Count();
            return i;
        }
        public string DeleteCountry(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryCountry.Single(a => a.DictionaryCountryId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Ghinion frate! Exista un judet in aceasta tara.";
            }
        }

    }
}
