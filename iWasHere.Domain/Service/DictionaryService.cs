﻿using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Hosting;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace iWasHere.Domain.Service
{
    public class DictionaryService
    {
        private readonly DatabaseContext _dbContext;
        private IHostingEnvironment _environment;
        public DictionaryService(DatabaseContext databaseContext, IHostingEnvironment environment)
        {
            _dbContext = databaseContext;
            _environment = environment;
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

        public List<DictionaryCountyTypeModel> GetDictionaryCountyTypeModelsFilter(int page, int pageSize, string name, int countryId, out int totalrows)
        {
            IQueryable<DictionaryCounty> query = _dbContext.DictionaryCounty;
            int skip = (page - 1) * pageSize;
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(a => a.DictionaryCountyName.StartsWith(name));
            }
            if (countryId != 0)
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


        public void InsertCity(DictionaryCityModel dictionaryCity)
        {
            DictionaryCity dic = new DictionaryCity();
            dic.DictionaryCityName = dictionaryCity.CityName;
            dic.DictionaryCountyId = dictionaryCity.CountyId;

            _dbContext.DictionaryCity.Add(dic);
            _dbContext.SaveChanges();
        }
        public void UpdateCity(DictionaryCityModel dictionaryCity)
        {
            DictionaryCity dic = new DictionaryCity();
            dic.DictionaryCityId = dictionaryCity.CityId;
            dic.DictionaryCityName = dictionaryCity.CityName;
            dic.DictionaryCountyId = dictionaryCity.CountyId;

            _dbContext.DictionaryCity.Update(dic);
            _dbContext.SaveChanges();
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
                return GetDictionaryCategoryTypeModel(page, pageSize);
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

        public List<DictionaryCityModel> GetDictionaryCityData(int page, int pageSize, string name, int countyId)
        {
            IQueryable<DictionaryCity> query = _dbContext.DictionaryCity;

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(a => a.DictionaryCityName.Contains(name));
            }

            if (countyId != 0)
            {
                query = query.Where(a => a.DictionaryCountyId == countyId);
            }

            int skip = (page - 1) * pageSize;

            List<DictionaryCityModel> dictionaryCityModels = query.Select(a => new DictionaryCityModel()
            {
                CityId = a.DictionaryCityId,
                CityName = a.DictionaryCityName,
                CountyName = a.DictionaryCounty.DictionaryCountyName,
                CountryName = a.DictionaryCounty.DictionaryCountry.DictionaryCountryName
            }).Skip(skip).Take(pageSize).ToList();

            return dictionaryCityModels;
        }

        public int FilterTotalCities(string name, int county)
        {
            if ((county <= 0 || county.ToString() == null) && String.IsNullOrEmpty(name) && String.IsNullOrWhiteSpace(name))
            {
                return _dbContext.DictionaryCity.Count();
            }
            else if (county <= 0 || county.ToString() == null)
            {
                return _dbContext.DictionaryCity.Where(a => a.DictionaryCityName.Contains(name)).Count();
            }
            else if (String.IsNullOrEmpty(name) && String.IsNullOrWhiteSpace(name))
            {
                return _dbContext.DictionaryCity.Where(a => a.DictionaryCountyId == county).Count();
            }
            else
            {
                return _dbContext.DictionaryCity.Where(a => a.DictionaryCityName.Contains(name) && a.DictionaryCountyId == county).Count();
            }
        }
        public List<DictionaryCityModel> GetDictionaryCityModels(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            List<DictionaryCityModel> dictionaryCityModels = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
            {
                CityId = a.DictionaryCityId,
                CityName = a.DictionaryCityName,
                CountyName = a.DictionaryCounty.DictionaryCountyName,
                CountryName = a.DictionaryCounty.DictionaryCountry.DictionaryCountryName
            }).Skip(skip).Take(pageSize).ToList();

            return dictionaryCityModels;


        }

        public List<DictionaryCountyModel> GetDictionaryCountiesCB(string text)
        {
            List<DictionaryCountyModel> dictionaryLCounties = null;

            IQueryable<DictionaryCounty> query = _dbContext.DictionaryCounty;

            if (!string.IsNullOrWhiteSpace(text))
                query = query.Where(a => a.DictionaryCountyName.Contains(text));

            dictionaryLCounties = query.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.DictionaryCountyId,
                CountyName = a.DictionaryCountyName

            }).Take(20).ToList();


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


        public DictionaryCityModel GetCity(int id)
        {

            DictionaryCityModel dictionaryCity = _dbContext.DictionaryCity
                .Where(a => a.DictionaryCityId == id)
                .Select(a => new DictionaryCityModel()
                {

                    CityId = a.DictionaryCityId,
                    CityName = a.DictionaryCityName,
                    CountyName = a.DictionaryCounty.DictionaryCountyName,
                    CountryName = a.DictionaryCounty.DictionaryCountry.DictionaryCountryName,
                    CountyId = a.DictionaryCountyId

                }).First();

            return dictionaryCity;
        }

        public int TotalCity()
        {
            int i = _dbContext.DictionaryCity.Count();
            return i;
        }
        public String DeleteCity(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryCity.Single(a => a.DictionaryCityId == id));

            }
            catch (Exception ex)
            {
                return "Exista un obiectiv turistic in acest oras.Nu poate fi sters.";
            }
            _dbContext.SaveChanges();
            return null;
        }
        public List<DictionaryCountyModel> Filter_GetCounties(string text)
        {
            var a = _dbContext.DictionaryCounty.Select(c => new DictionaryCountyModel()
            {
                CountyId = c.DictionaryCountyId,
                CountyName = c.DictionaryCountyName

            });
            if (!string.IsNullOrEmpty(text))
            {
                a = a.Where(p => p.CountyName.StartsWith(text));
            }
            List<DictionaryCountyModel> countyListModels = a.Take(50).ToList();

            return countyListModels;

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
        public List<DictionarySeasonModel> GetTouristAttractionsSeasonSchedule()
        {
            List<DictionarySeasonModel> seasons = _dbContext.DictionarySeason.Select(a => new DictionarySeasonModel()
            {
                SeasonId = a.DictionarySeasonId,
                SeasonName = a.DictionarySeasonName

            }).ToList();
            return seasons;
        }

        public List<TouristAttractionScheduleModel> GetTouristAttraction()
        {
            List<TouristAttractionScheduleModel> touristAttractionSchedule = _dbContext.TouristAttraction.Select(a => new TouristAttractionScheduleModel()
            {
                TouristAttractionId = a.TouristAttractionId,
                TouristAttractionName = a.Name

            }).ToList();

            return touristAttractionSchedule;
        }

        public ScheduleTouristAttractionModel GetScheduleValueById(int scheduleId)
        {

            ScheduleTouristAttractionModel scheduleTouristAttractionModel = _dbContext.Schedule.Where(a => a.ScheduleId == scheduleId)
                .Select(a => new ScheduleTouristAttractionModel()
                {
                    Day = a.Day,
                    StartHour = a.StartHour,
                    EndHour = a.EndHour,
                    Season = a.Season.DictionarySeasonName,
                    TouristAttractionName = a.TouristAttraction.Name,
                    SeasonId = a.SeasonId,
                    TouristAttractionId = a.TouristAttractionId
                }).First();



            return scheduleTouristAttractionModel;
        }

        public List<ScheduleTouristAttractionModel> GetDictionaryScheduleModels(int page, int pageSize)
        {
            int pageSkip = (page - 1) * pageSize;
            List<ScheduleTouristAttractionModel> scheduleTouristAttractions = new List<ScheduleTouristAttractionModel>();

            scheduleTouristAttractions = _dbContext.Schedule.
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

        public List<CountryListModel> Filter_GetCountries(string text)
        {
            var a = _dbContext.DictionaryCountry.Select(c => new CountryListModel()
            {
                Id = c.DictionaryCountryId,
                Name = c.DictionaryCountryName
            });
            if (!string.IsNullOrEmpty(text))
            {
                a = a.Where(p => p.Name.StartsWith(text));
            }
            List<CountryListModel> countryListModels = a.Take(50).ToList();
            return countryListModels;
        }
        public List<ScheduleTouristAttractionModel> GetDictionaryScheduleFiltred(int page, int pageSize, string searchString)
        {

            int pageSkip = (page - 1) * pageSize;
            List<ScheduleTouristAttractionModel> schedulefiltered = _dbContext.Schedule
                .Where(a => a.TouristAttraction.Name == searchString || a.TouristAttraction.Name.StartsWith(searchString)).
                Select(a => new ScheduleTouristAttractionModel()
                {
                    ScheduleId = a.ScheduleId,
                    Day = a.Day,
                    StartHour = a.StartHour,
                    EndHour = a.EndHour,
                    Season = a.Season.DictionarySeasonName,
                    TouristAttractionName = a.TouristAttraction.Name


                }).Skip(pageSkip).Take(pageSize).ToList();


            return schedulefiltered;
        }


        public string UpdateSchedule(ScheduleTouristAttractionModel scheduleTourist)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Convert.ToString(scheduleTourist.TouristAttractionId)))
                {
                    return "Obiectivul turistic  este obligatoriu";
                }
                else
                {
                    Schedule schedule = _dbContext.Schedule.Find(scheduleTourist.ScheduleId);
                    schedule.ScheduleId = scheduleTourist.ScheduleId;
                    schedule.TouristAttractionId = scheduleTourist.TouristAttractionId;
                    schedule.SeasonId = scheduleTourist.SeasonId;
                    schedule.Day = scheduleTourist.Day;
                    schedule.StartHour = scheduleTourist.StartHour;
                    schedule.EndHour = scheduleTourist.EndHour;
                    schedule.Season = _dbContext.DictionarySeason.Find(scheduleTourist.SeasonId);
                    schedule.TouristAttraction = _dbContext.TouristAttraction.Find(scheduleTourist.TouristAttractionId);
                    _dbContext.Schedule.Update(schedule);
                    _dbContext.SaveChanges();
                    return null;
                }
            }
            catch (Exception e)
            {
                return "Trebuie sa completati campurile!";
            }
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

        public List<ScheduleTouristAttractionModel> GetDictionaryScheduleModels(int page, int pageSize, string searchString)
        {

            if (!string.IsNullOrEmpty(searchString))
            {
                int pageSkip = (page - 1) * pageSize;
                List<ScheduleTouristAttractionModel> scheduleTouristAttractions = _dbContext.Schedule
                     .Where(a => a.TouristAttraction.Name.Contains(searchString)).
                     Select(a => new ScheduleTouristAttractionModel()
                     {
                         ScheduleId = a.ScheduleId,
                         Day = a.Day,
                         StartHour = a.StartHour,
                         EndHour = a.EndHour,
                         Season = a.Season.DictionarySeasonName,
                         TouristAttractionName = a.TouristAttraction.Name

                     }).Skip(pageSkip).Take(pageSize).ToList();
                return scheduleTouristAttractions;
            }
            else
            {
                int pageSkip1 = (page - 1) * pageSize;
                List<ScheduleTouristAttractionModel> scheduleTouristAttractions = _dbContext.Schedule.
                Select(a => new ScheduleTouristAttractionModel()
                {
                    ScheduleId = a.ScheduleId,
                    Day = a.Day,
                    StartHour = a.StartHour,
                    EndHour = a.EndHour,
                    Season = a.Season.DictionarySeasonName,
                    TouristAttractionName = a.TouristAttraction.Name

                }).Skip(pageSkip1).Take(pageSize).ToList();
                return scheduleTouristAttractions;

            }
        }

        public string DeleteSchedule(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.Schedule.Single(a => a.ScheduleId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Acest program nu poate fi sters pentru ca are asociat un muzeu.";

            }
        }


        public int GetItemsOfSchedule()
        {
            return _dbContext.Schedule.Count();
        }

        public int GetItemsOfSchedule(string searchText)
        {

            if (!string.IsNullOrEmpty(searchText))

                return _dbContext.Schedule.Where(a => a.TouristAttraction.Name.Contains(searchText)).Count();
            else
                return _dbContext.Schedule.Count();
        }
        /*
        public List<DictionaryCountryModel> GetDictionaryCountryModel(int page, int pageSize)
        {

            int pageSkip = (page - 1) * pageSize;
            List<ScheduleTouristAttractionModel> schedulefiltered = _dbContext.Schedule
                .Where(a => a.TouristAttraction.Name == searchString || a.TouristAttraction.Name.StartsWith(searchString)).
                Select(a => new ScheduleTouristAttractionModel()
                {
                    ScheduleId = a.ScheduleId,
                    Day = a.Day,
                    StartHour = a.StartHour,
                    EndHour = a.EndHour,
                    Season = a.Season.DictionarySeasonName,
                    TouristAttractionName = a.TouristAttraction.Name



                }).Skip(pageSkip).Take(pageSize).ToList();




            return countries;

            int pageSkip = (page - 1) * pageSize;
            List<ScheduleTouristAttractionModel> schedulefiltered = _dbContext.Schedule
                .Where(a => a.TouristAttraction.Name == searchString || a.TouristAttraction.Name.StartsWith(searchString)).
                Select(a => new ScheduleTouristAttractionModel()
                {
                    ScheduleId = a.ScheduleId,
                    Day = a.Day,
                    StartHour = a.StartHour,
                    EndHour = a.EndHour,
                    Season = a.Season.DictionarySeasonName,
                    TouristAttractionName = a.TouristAttraction.Name

                }).Skip(pageSkip).Take(pageSize).ToList();

            return schedulefiltered;
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
            catch
            {
                return " Aceasta tara nu poate fi stearsa!Exista un judet in aceasta tara.";
            }
        }

        public int countAtractionsonCity(int id)
        {
            int i = _dbContext.TouristAttraction.Where(a => a.CityId == id).Count();
            return i;
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
                    Where(a => a.DictionaryCurrencyName.Contains(txtFilterName))
                    //Where(a => a.DictionaryCurrencyName == txtFilterName)
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
                return _dbContext.DictionaryCurrency.Where(a => a.DictionaryCurrencyName.Contains(txtFilterName)).Count();
            else
                return _dbContext.DictionaryCurrency.Count();

        }
        public void AddDictionaryCurrency(DictionaryCurrency dictionaryCurrency)
        {
            _dbContext.DictionaryCurrency.Add(dictionaryCurrency);
            _dbContext.SaveChanges();
        }
        public string DeleteCurrency(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryCurrency.Single(a => a.DictionaryCurrencyId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return "Te pup pe portofel!";
                return null;
            }
        }
        public string InsertNewSchedule(ScheduleTouristAttractionModel scheduleTourist)
        {
            string message;
            if (!ValidateData(scheduleTourist, out message))
            {
                return message;
            }

            try
            {
                Schedule schedule = new Schedule();
                schedule.TouristAttractionId = scheduleTourist.TouristAttractionId;
                schedule.SeasonId = scheduleTourist.SeasonId;
                schedule.Day = scheduleTourist.Day;
                schedule.StartHour = scheduleTourist.StartHour;
                schedule.EndHour = scheduleTourist.EndHour;
                _dbContext.Schedule.Add(schedule);
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception e)
            {
                return "Trebuie sa completati programul!";
            }
        }
        public DictionaryCountry AddDictionaryCountry(DictionaryCountry country)
        {
            if (country.DictionaryCountryId != null)
                if (!string.IsNullOrWhiteSpace(country.DictionaryCountryName))
                {
                    _dbContext.Add(country);
                    _dbContext.SaveChanges();
                }
            return null;
        }

        public TouristAttractionMapsModel GetTouristAttractionMapsById(int Id)
        {
            try
            {
                TouristAttractionMapsModel scheduleTouristAttractionModel = _dbContext.TouristAttraction.Where(a => a.TouristAttractionId == Id)
               .Select(a => new TouristAttractionMapsModel()
               {

                   TouristAttractionId = a.TouristAttractionId,
                   Latitude = a.Latitudine,
                   Longitude = a.Longtitudine,
                   Reviews = _dbContext.Review.Where(b => b.TouristAttractionId == Id).Select(x => new ReviewModel()
                   {
                       RatingValue = x.Rating + 1,
                       Comment = x.Comment,
                       Title = x.Title,
                       User = x.UserName,

                       TouristAttractionId = x.TouristAttractionId
                   }).ToList()

               }).First();

                return scheduleTouristAttractionModel;

            }
            catch(Exception e)
            {
                return null;
            }
           

        }

        public string InsertReview(ReviewModel model)
        {
            try
            {
                Review review = new Review();
                review.UserId = "6492e4c6-bef2-4617-922e-bf54e5f4efe8";
                review.UserName = model.User;
                review.TouristAttractionId = model.TouristAttractionId;
                review.Comment = model.Comment;
                review.Rating = model.RatingValue;
                review.Title = model.Title;

                _dbContext.Review.Add(review);
                _dbContext.SaveChanges();

                return null;
            }
            catch (Exception e)
            {
                return "nu a mers";
            }


        }

        public DictionaryCountyTypeModel getCountyIdUpdate(int id)
        {
            DictionaryCountyTypeModel dictionaryCountyTypeModel = _dbContext.DictionaryCounty.Where(a => a.DictionaryCountyId == id)
                .Select(a => new DictionaryCountyTypeModel()
                {
                    Id = a.DictionaryCountyId,
                    Name = a.DictionaryCountyName,
                    CountryName = a.DictionaryCountry.DictionaryCountryName,
                    CountryId = a.DictionaryCountryId
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
                    _dbContext.SaveChanges(); return null;
                }
            }
            catch (Exception e)
            {
                return "Trebuie sa completati campurile";
            }
        }

        public DictionaryCurrency GetCurrency(int id)
        {

            return _dbContext.DictionaryCurrency.Where(a => a.DictionaryCurrencyId == id).FirstOrDefault();
        }
        public void EditDictionaryCurrency(DictionaryCurrency dict)
        {
            _dbContext.Update(dict);
            _dbContext.SaveChanges();
        }
        public string UpdateCounty(DictionaryCountyTypeModel dictionaryCountyTypeModel)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(dictionaryCountyTypeModel.Name))
                {
                    return "Judetul trebuie completat";
                }
                else
                {
                    DictionaryCounty dictionaryCounty = _dbContext.DictionaryCounty.Find(dictionaryCountyTypeModel.Id);
                    dictionaryCounty.DictionaryCountyName = dictionaryCountyTypeModel.Name;
                    dictionaryCounty.DictionaryCountryId = dictionaryCountyTypeModel.CountryId;
                    dictionaryCounty.DictionaryCountyId = dictionaryCountyTypeModel.Id;
                    _dbContext.DictionaryCounty.Update(dictionaryCounty);
                    _dbContext.SaveChanges();
                    return null;
                }
            }
            catch (Exception e)
            {
                return "Trebuie sa completati campurile";
            }
        }
        public List<TouristAttractionsDTO> GetTouristAttractionsModel(int page, int pageSize, string txtFilterName)
        {
            if (!string.IsNullOrEmpty(txtFilterName))
            {
                List<TouristAttractionsDTO> touristAttraction = _dbContext.TouristAttraction.Include(a => a.Category).Include(a => a.City).Include(a => a.Landmark).
                    Where(a => a.Name.Contains(txtFilterName))
                    //Where(a => a.DictionaryCurrencyName == txtFilterName)
                    .Select(a => new TouristAttractionsDTO()
                    {
                        TouristAttractionId = a.TouristAttractionId,
                        Name = a.Name,
                        Description = a.Description,
                        Longtitudine = a.Longtitudine,
                        Latitudine = a.Latitudine,
                        CityName = a.City.DictionaryCityName,
                        LandmarkName = a.Landmark.DictionaryItemName,
                        CategoryName = a.Category.DictionaryCategoryName,
                    }).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return touristAttraction;
            }
            else
            {
                List<TouristAttractionsDTO> touristAttraction = _dbContext.TouristAttraction.Select(a => new TouristAttractionsDTO()
                {
                    TouristAttractionId = a.TouristAttractionId,
                    Name = a.Name,
                    Description = a.Description,
                    Longtitudine = a.Longtitudine,
                    Latitudine = a.Latitudine,
                    CityName = a.City.DictionaryCityName,
                    LandmarkName = a.Landmark.DictionaryItemName,
                    CategoryName = a.Category.DictionaryCategoryName,
                }).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return touristAttraction;
            }
        }
        public TouristAttraction GetTouristAttractions(int id)
        {
            return _dbContext.TouristAttraction.Include(a => a.Category).Include(a => a.City).Include(a => a.Landmark).Where(a => a.TouristAttractionId == id).FirstOrDefault();
        }
        public int AddTouristAttractions(TouristAttraction touristAttraction)
        {
            _dbContext.TouristAttraction.Add(touristAttraction);
            _dbContext.SaveChanges();
            int id = touristAttraction.TouristAttractionId;
            return id;
        }
        public int GetCountTouristAttraction(string txtFilterName)
        {
            if (!string.IsNullOrEmpty(txtFilterName))
                return _dbContext.TouristAttraction.Where(a => a.Name.Contains(txtFilterName)).Count();
            else
                return _dbContext.TouristAttraction.Count();

        }
        public List<DictionaryCategoryTypeModel> GetTouristAttractionsCategory()
        {
            List<DictionaryCategoryTypeModel> seasons = _dbContext.DictionaryCategory.Select(a => new DictionaryCategoryTypeModel()
            {
                Id = a.DictionaryCategoryId,
                Name = a.DictionaryCategoryName
            }).ToList();
            return seasons;
        }
        public List<ScheduleTouristAttractionModel> GetTouristAttractionsSchedule()
        {
            List<ScheduleTouristAttractionModel> seasons = _dbContext.Schedule.Select(a => new ScheduleTouristAttractionModel()
            {
                ScheduleId = a.ScheduleId,
                Day = a.Day,
                StartHour = a.StartHour,
                EndHour = a.EndHour,
                Season = a.Season.DictionarySeasonName,
                TouristAttractionName = a.TouristAttraction.Name,
                SeasonId = a.SeasonId,
                TouristAttractionId = a.TouristAttractionId
            }).ToList();
            return seasons;
        }
        public List<DictionaryCountry> GetTouristAttractionsCountry()
        {
            List<DictionaryCountry> dc = _dbContext.DictionaryCountry.Select(a => new DictionaryCountry()
            {
                DictionaryCountryId = a.DictionaryCountryId,
                DictionaryCountryCode = a.DictionaryCountryCode,
                DictionaryCountryName = a.DictionaryCountryName,
                DictionaryCounty = a.DictionaryCounty
            }).ToList();
            return dc;
        }


        public List<DictionaryCity> GetTouristAttractionsCity(string text)
        {
            if ((string.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text)))
            {
                return _dbContext.DictionaryCity.Select(c => new DictionaryCity()
                {
                    DictionaryCityId = c.DictionaryCityId,
                    DictionaryCityName = c.DictionaryCityName,
                    DictionaryCounty = c.DictionaryCounty,
                    DictionaryCountyId = c.DictionaryCountyId,
                    TouristAttraction = c.TouristAttraction
                }).Take(50).ToList();
            }
            else
            {
                return _dbContext.DictionaryCity.Where(p => p.DictionaryCityName.Contains(text)).Select(c => new DictionaryCity()
                {
                    DictionaryCityId = c.DictionaryCityId,
                    DictionaryCityName = c.DictionaryCityName,
                    DictionaryCounty = c.DictionaryCounty,
                    DictionaryCountyId = c.DictionaryCountyId,
                    TouristAttraction = c.TouristAttraction
                }).Take(50).ToList();
            }
        }
        public DictionaryCategory GetDictionaryCategory(int id)
        {
            return _dbContext.DictionaryCategory.Find(id);
        }
        public DictionaryCity GetDictionaryCity(int id)
        {
            return _dbContext.DictionaryCity.Find(id);
        }
        public DictionaryLandmarkType GetLandmark(int id)
        {
            return _dbContext.DictionaryLandmarkType.Find(id);
        }
        public void EditTouristAttractions(TouristAttraction dict)
        {
            _dbContext.TouristAttraction.Update(dict);
            _dbContext.SaveChanges();
        }
        public List<String> GetImages(int id)
        {
            List<Image> imagini = _dbContext.Image.Where(a => a.TouristAttractionId == id).Select(a => new Image()
            {

                Path = a.Path,
            }).ToList();
            List<String> img = new List<String>();
            for (int i = 0; i < imagini.Count; i++)
            {
                img.Add(imagini[i].Path);
            }
            return img;
        }

        public List<DictionaryLandmarkType> GetTouristAttractionsLandmark(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkType()
                {
                    DictionaryItemId = a.DictionaryItemId,
                    DictionaryItemCode = a.DictionaryItemCode,
                    DictionaryItemName = a.DictionaryItemName,
                    Description = a.Description,
                    TouristAttraction = a.TouristAttraction
                }).Take(50).ToList();
            }
            else
            {
                List<DictionaryLandmarkType> dL = _dbContext.DictionaryLandmarkType.Where(p => p.DictionaryItemName.Contains(text))
                    .Select(a => new DictionaryLandmarkType()
                    {
                        DictionaryItemId = a.DictionaryItemId,
                        DictionaryItemCode = a.DictionaryItemCode,
                        DictionaryItemName = a.DictionaryItemName,
                        Description = a.Description,
                        TouristAttraction = a.TouristAttraction
                    }).ToList();
                return dL;
            }
        }
        public List<DictionaryLandmarkType> GetTouristAttractionsLandmark_v2()
        {
            List<DictionaryLandmarkType> dL = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkType()
            {
                DictionaryItemId = a.DictionaryItemId,
                DictionaryItemCode = a.DictionaryItemCode,
                DictionaryItemName = a.DictionaryItemName,
                Description = a.Description,
                TouristAttraction = a.TouristAttraction
            }).ToList();
            return dL;
        }
        public string DeleteTouristAttraction(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.TouristAttraction.Single(a => a.TouristAttractionId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return "Nu se poate sterge!";
                return null;
            }
        }


       

        public bool ValidateData(ScheduleTouristAttractionModel model, out string message)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(model.Day))
                {
                    message = "Ziua este obligatorie";
                    return false;
                }

                else if (model.SeasonId == 0)
                {
                    message = "Nu ai selectat tipul de sezonalitate";
                    return false;
                }
                else if (model.TouristAttractionId == 0)
                {
                    message = "Nu ai selectat obiectivul turistic";
                    return false;
                }
                //else if (Convert.ToInt32(model.StartHour)==0)
                //{
                //    message = "Nu ai ora de inceput";
                //    return false;
                //}
                //else if (Convert.ToInt32(model.EndHour)==0)
                //{
                //    message = "Nu ai selectat ora de sfarsit";
                //    return false;
                //}
            }
            catch (Exception e)
            {

            }

            message = null;
            return true;
        }

        public void AddImage(Image image)
        {
            _dbContext.Add(image);
            _dbContext.SaveChanges();
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

        public string InsertNewCounty(DictionaryCountyTypeModel dictionaryCounty)
        {
            try
            {
                DictionaryCounty dic = new DictionaryCounty();
                dic.DictionaryCountyName = dictionaryCounty.Name;
                dic.DictionaryCountryId = dictionaryCounty.CountryId;
                _dbContext.DictionaryCounty.Add(dic);
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception e)
            {
                return "Trebuie sa completati campurile";
            }
        }
            public List<TouristAttractionsDTO> GetTouristAttractionsByCountry(int id,int page, int pageSize)
        {
            

            List<TouristAttractionsDTO> touristAttraction = _dbContext.TouristAttraction
                .Include(a => a.City)
                .ThenInclude(a => a.DictionaryCounty)
                .ThenInclude(a => a.DictionaryCountry)
                .Include(a => a.Category)
                .Include(a => a.Landmark)
                .Include(a => a.Image)
               
                .Where(b => b.City.DictionaryCounty.DictionaryCountry.DictionaryCountryId == id )
                .Select(a => new TouristAttractionsDTO()
                {
                 
                        TouristAttractionId = a.TouristAttractionId,
                        Name = a.Name,
                        Description = a.Description,
                        Longtitudine = a.Longtitudine,
                        Latitudine = a.Latitudine,
                        CityName = a.City.DictionaryCityName,
                        LandmarkName = a.Landmark.DictionaryItemName,
                        CategoryName = a.Category.DictionaryCategoryName,
                    //FirstPhotoPath = (a.Image.ToList())[0].Path,
                    FirstPhotoPath = "27620ead-7f5b-4e39-ab04-7acd0d77d694_noImage.png",
                }).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    return touristAttraction;
                }

        public int TouristAttractionsbyCountryCount(int id) {
            return _dbContext.TouristAttraction.Include(a => a.City)
                .ThenInclude(a => a.DictionaryCounty)
                .ThenInclude(a => a.DictionaryCountry)
                .Include(a => a.Category)
                .Include(a => a.Landmark)
                .Include(a => a.Image)
                .Where(b => b.City.DictionaryCounty.DictionaryCountry.DictionaryCountryId == id).Count();
        }
        public DictionaryCounty GetCounty(int id)
        {
            DictionaryCounty dictionaryCounty = _dbContext.DictionaryCounty.Include(a => a.DictionaryCountry).Where(a => a.DictionaryCountyId == id).FirstOrDefault();
            return dictionaryCounty;
        }

        public Stream SaveDataInWord(TouristAttraction tA)
        {
            string photoWord = null;
            List<ReviewModel> reviewModel = new List<ReviewModel>();
            if (_dbContext.Review.Where(a => a.TouristAttractionId == tA.TouristAttractionId).Count() > 0)
            {
                reviewModel = _dbContext.Review.Where(a => a.TouristAttractionId == tA.TouristAttractionId).Select(a => new ReviewModel()
                {
                    ReviewId = a.ReviewId,
                    Title = a.UserName,
                    Comment = a.Comment,
                    RatingValue = a.Rating
                }).ToList();
            }
            if (_dbContext.Image.Where(a => a.TouristAttractionId == tA.TouristAttractionId).Count() > 0)
            {
                Image photo = _dbContext.Image.First(a => a.TouristAttractionId == tA.TouristAttractionId);
                photoWord = photo.Path;
            }
            var stream = new MemoryStream();

            using (WordprocessingDocument doc = WordprocessingDocument.Create(stream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))

            {
                MainDocumentPart mainPart = doc.AddMainDocumentPart();

                new DocumentFormat.OpenXml.Wordprocessing.Document(new Body()).Save(mainPart);
                //if (photoWord != null)
                //{
                //    ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);

                //    using (FileStream imgstream = new FileStream(_environment.WebRootPath + "\\images\\" + photoWord.Substring(1), FileMode.Open))
                //    //using (FileStream imgstream = new FileStream("images/" + photoWord.Substring(1), FileMode.Open))
                //    {
                //        imagePart.FeedData(imgstream);
                //    }
                //    AddImageToBody(doc, mainPart.GetIdOfPart(imagePart));
                //}
                Body body = mainPart.Document.Body;
                body.Append(
                      new Body(
                      new Paragraph(
                        new Run(
                          new Text("Numele obiectivului: " + tA.Name))),
                      new Paragraph(
                        new Run(
                          new Text("\n Descriere: " + tA.Description))),

                      new Paragraph(
                        new Run(
                              new Text("\n Orasul: " + tA.City.DictionaryCityName))),

                      new Paragraph(
                        new Run(
                              new Text("\n Judetul: " + tA.City.DictionaryCounty.DictionaryCountyName))),
                      new Paragraph(
                        new Run(
                              new Text("\n Tara: " + tA.City.DictionaryCounty.DictionaryCountry.DictionaryCountryName)))
                            ));
                int sum = 0;
                if (reviewModel.Count() > 0)
                {
                    foreach (ReviewModel reviewM in reviewModel)
                    {
                        sum = sum + reviewM.RatingValue;
                        body.Append(new Paragraph(
                            new Run(
                              new Text("Comentarii: " + reviewM.Comment))));
                    }
                    body.Append(new Paragraph(
                           new Run(
                             new Text("Medie rating: " + sum / reviewModel.Count()))));
                }
                mainPart.Document.Save();
                doc.Save();
                doc.Close();
            }
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        private static void AddImageToBody(WordprocessingDocument wordDoc, string relationshipId)
        {
            // Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = 990000L, Cy = 792000L },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = "Picture 1"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = "New Bitmap Image.jpg"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         )
                                         {
                                             Embed = relationshipId,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = 990000L, Cy = 792000L }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         )
                                         { Preset = A.ShapeTypeValues.Rectangle }))
                             )
                             { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                         EditId = "50D07946"
                     });

            // Append the reference to body, the element should be in a Run.
            wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
        }

        public bool CheckImageFromDB(int Id, out string abc)
        {
            Image img = _dbContext.Image.Where(a => a.TouristAttractionId == Id).FirstOrDefault();
            if (img != null)
            {
                abc = img.Path;
                return true;
            }
            abc = "";
            return false;
        }
    }

      



}
 
