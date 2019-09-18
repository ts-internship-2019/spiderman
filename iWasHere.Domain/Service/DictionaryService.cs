using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
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
          
            int skip = (page - 1) * pageSize;
            List<DictionaryCountyTypeModel> dictionaryCountyTypeModel = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyTypeModel()
            {
                Id = a.DictionaryCountyId,
                Name = a.DictionaryCountyName,
                CountryName=a.DictionaryCountry.DictionaryCountryName
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

        public List<DictionaryCountyTypeModel> GetDictionaryCountyTypeModelsFilter(int page, int pageSize, string name, int countryId,out int totalrows)
        {
            IQueryable<DictionaryCounty> query = _dbContext.DictionaryCounty;
            int skip = (page - 1) * pageSize;
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(a => a.DictionaryCountyName.StartsWith(name));
            }
            if (countryId!=0)
            {
                query = query.Where(a => a.DictionaryCountryId == countryId);
            }

            totalrows = query.Count();
            List<DictionaryCountyTypeModel> dictionaryCountyTypeModel = query.Select(a => new DictionaryCountyTypeModel()
            {
                Id = a.DictionaryCountyId,
                Name = a.DictionaryCountyName,
                CountryName = a.DictionaryCountry.DictionaryCountryName
            }).Skip(skip).Take(pageSize).ToList();
       
               
            
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

        public int Total()
        {
            int i = _dbContext.DictionaryCategory.Count();
            return i;
        }

        public int TotalCounty()
        {
            int i = _dbContext.DictionaryCounty.Count();
            return i;
        }

        public List<DictionaryCityModel> GetDictionaryCityModels(int page,int pageSize)
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
             CountyId=a.DictionaryCountyId,
            CountyName=a.DictionaryCountyName

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

        public List<CountryListModel>Filter_GetCountries(string text)
        {
            var a = _dbContext.DictionaryCountry.Select(c => new CountryListModel()
            {
                Id =c.DictionaryCountryId,
                Name=c.DictionaryCountryName
            });
            if (!string.IsNullOrEmpty(text))
            {
                a = a.Where(p => p.Name.StartsWith(text));
            }
            List<CountryListModel> countryListModels = a.Take(50).ToList();

            return countryListModels;

        }


        public int FilterTotalCounties(string name, int country)
        {
            int i = _dbContext.DictionaryCounty.Where(a => a.DictionaryCountyName == name && a.DictionaryCountryId == country).Count();
            return i;
        }

        public string DeleteCounty(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryCounty.Single(a => a.DictionaryCountyId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Acest judet nu poate fi sters pentru ca are asociat un oras!!!";
            }
        }

    }
}
