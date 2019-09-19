﻿using iWasHere.Domain.DTOs;
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

        public List<DictionaryCountyTypeModel> GetDictionaryCountyTypeModelsFilter(int page, int pageSize, string name)
        {
            //pageSize = 10;
            int skip = (page - 1) * pageSize;
            List<DictionaryCountyTypeModel> dictionaryCountyTypeModel = 
                _dbContext.DictionaryCounty
                .Where(a=>a.DictionaryCountyName==name)
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

        public int Total()
        {
            int i = _dbContext.DictionaryCategory.Count();
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
        public int TotalCity()
        {
            int i = _dbContext.DictionaryCity.Count();
            return i;
        }




        public List<ScheduleTouristAttractionModel> GetDictionaryScheduleModels(int page,int pageSize)
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





        public List<DictionaryCurrencyModel> GetDictionaryCurrencyModel()
        {
            List<DictionaryCurrencyModel> dictionaryCurrencyModel = _dbContext.DictionaryCurrency.
                Select(a => new DictionaryCurrencyModel()
            {
                
                DictionaryItemId = a.DictionaryCurrencyId,
                DictionaryItemCode = a.DictionaryCurrencyCode,
                DictionaryItemName = a.DictionaryCurrencyName
                }).ToList();

            return dictionaryCurrencyModel;
        }
        public List<DictionaryCurrencyModel> GetDictionaryCurrencyModel(int page, int pageSize)
        {
            List<DictionaryCurrencyModel> dictionaryCurrencyModel = _dbContext.DictionaryCurrency.Select(a => new DictionaryCurrencyModel()
            {
                DictionaryItemId = a.DictionaryCurrencyId,
                DictionaryItemCode = a.DictionaryCurrencyCode,
                DictionaryItemName = a.DictionaryCurrencyName
            }).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return dictionaryCurrencyModel;
        }
        public List<DictionaryCurrencyModel> GetDictionaryCurrencyModel(int page, int pageSize, string txtFilterName)
        {
            if (!string.IsNullOrEmpty(txtFilterName))
            {
                List<DictionaryCurrencyModel> dictionaryCurrencyModel = _dbContext.DictionaryCurrency.
                    //Where(a => a.DictionaryCurrencyName.Contains(txtNameFilter))
                    Where(a => a.DictionaryCurrencyName == txtFilterName)
                    .Select(a => new DictionaryCurrencyModel()
                    {
                        DictionaryItemId = a.DictionaryCurrencyId,
                        DictionaryItemCode = a.DictionaryCurrencyCode,
                        DictionaryItemName = a.DictionaryCurrencyName
                    }).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return dictionaryCurrencyModel;
            }
            else
            {
                List<DictionaryCurrencyModel> dictionaryCurrencyModel = _dbContext.DictionaryCurrency.Select(a => new DictionaryCurrencyModel()
                    {
                        DictionaryItemId = a.DictionaryCurrencyId,
                        DictionaryItemCode = a.DictionaryCurrencyCode,
                        DictionaryItemName = a.DictionaryCurrencyName
                    }).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return dictionaryCurrencyModel;
            }
        }
        public int GetCountDictionaryCurrency()
        {
            return _dbContext.DictionaryCurrency.Count();
        }
        public int GetCountDictionaryCurrency(string txtFilterName)
        {
            if (!string.IsNullOrEmpty(txtFilterName))
                return _dbContext.DictionaryCurrency.Where(a => a.DictionaryCurrencyName == txtFilterName).Count();
            else
                return _dbContext.DictionaryCurrency.Count();

        }

        public void AddDictionaryCurrency(DictionaryCurrency dictionaryCurrency)
        {
            _dbContext.DictionaryCurrency.Add(dictionaryCurrency);
        }
    }
}
