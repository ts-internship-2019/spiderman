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
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Exista un obiectiv turistic in acest oras.Nu poate fi sters.";
            }
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


        public ScheduleTouristAttractionModel GetScheduleValueById(int scheduleId)
        {
            //Schedule var1 = _dbContext.Schedule.Where(a => a.ScheduleId == scheduleId).FirstOrDefault();
            //ScheduleTouristAttractionModel var2 = new ScheduleTouristAttractionModel();
            //var2.ScheduleId = var1.ScheduleId;
            //var2.Day = var1.Day;
            //var2.StartHour = var1.StartHour;
            //var2.TouristAttractionId = var1.TouristAttractionId;
            //var2.TouristAttractionName = var1.TouristAttraction.Name;
            //var2.SeasonId = var1.SeasonId;
            //var2.Season = var1.Season.DictionarySeasonName;
            //return var2;

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

                return _dbContext.Schedule.Where(a => a.TouristAttraction.Name == searchText).Count();
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
        public List<TouristAttraction> GetTouristAttractionsModel(int page, int pageSize, string txtFilterName)
        {
            if (!string.IsNullOrEmpty(txtFilterName))
            {
                List<TouristAttraction> touristAttraction = _dbContext.TouristAttraction.
                    Where(a => a.Name.Contains(txtFilterName))
                    //Where(a => a.DictionaryCurrencyName == txtFilterName)
                    .Select(a => new TouristAttraction()
                    {
                        TouristAttractionId = a.TouristAttractionId,
                        Name = a.Name,
                        Description = a.Description,
                        Longtitudine = a.Longtitudine,
                        Latitudine = a.Latitudine,
                        CityId = Convert.ToInt32(a.CityId),
                        LandmarkId = Convert.ToInt32(a.LandmarkId),
                        CategoryId = Convert.ToInt32(a.CategoryId)
                    }).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                for (int i = 0; i < touristAttraction.Count; i++)
                {
                    touristAttraction[i].City = new DictionaryCity();
                    touristAttraction[i].Category = new DictionaryCategory();
                    touristAttraction[i].Landmark = new DictionaryLandmarkType();
                    touristAttraction[i].City = _dbContext.DictionaryCity.Find(touristAttraction[i].CityId);
                    touristAttraction[i].City.DictionaryCounty = _dbContext.DictionaryCounty.Find(touristAttraction[i].City.DictionaryCountyId);
                    touristAttraction[i].Category = _dbContext.DictionaryCategory.Find(touristAttraction[i].CategoryId);
                    touristAttraction[i].Landmark = _dbContext.DictionaryLandmarkType.Find(touristAttraction[i].LandmarkId);
                }
                return touristAttraction;
            }
            else
            {
                List<TouristAttraction> touristAttraction = _dbContext.TouristAttraction.Select(a => new TouristAttraction()
                {
                    TouristAttractionId = a.TouristAttractionId,
                    Name = a.Name,
                    Description = a.Description,
                    Longtitudine = a.Longtitudine,
                    Latitudine = a.Latitudine,
                    CityId = Convert.ToInt32(a.CityId),
                    LandmarkId = Convert.ToInt32(a.LandmarkId),
                    CategoryId = Convert.ToInt32(a.CategoryId)
                }).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                for (int i = 0; i < touristAttraction.Count; i++)
                {
                    touristAttraction[i].City = new DictionaryCity();
                    touristAttraction[i].Category = new DictionaryCategory();
                    touristAttraction[i].Landmark = new DictionaryLandmarkType();
                    touristAttraction[i].City = _dbContext.DictionaryCity.Find(touristAttraction[i].CityId);
                    touristAttraction[i].City.DictionaryCounty = _dbContext.DictionaryCounty.Find(touristAttraction[i].City.DictionaryCountyId);
                    touristAttraction[i].Category = _dbContext.DictionaryCategory.Find(touristAttraction[i].CategoryId);
                    touristAttraction[i].Landmark = _dbContext.DictionaryLandmarkType.Find(touristAttraction[i].LandmarkId);
                }
                return touristAttraction;
            }
        }
        public TouristAttraction GetTouristAttractions(int id)
        {
            return _dbContext.TouristAttraction.Where(a => a.TouristAttractionId == id).FirstOrDefault();
        }
        public void AddTouristAttractions(TouristAttraction touristAttraction)
        {
            _dbContext.TouristAttraction.Add(touristAttraction);
            _dbContext.SaveChanges();
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
                return _dbContext.DictionaryCity.Where(p => p.DictionaryCityName.StartsWith(text)).Select(c => new DictionaryCity()
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
    }
}
