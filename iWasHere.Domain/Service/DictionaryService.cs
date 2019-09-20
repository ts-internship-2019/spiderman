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

        public List<TouristAttractionScheduleModel> GetTouristAttractionsSchedule()
        {
            List<TouristAttractionScheduleModel> touristAttractionScheduleModels = _dbContext.TouristAttraction.Select(a => new TouristAttractionScheduleModel()
            {
                TouristAttractionId = a.TouristAttractionId,
                TouristAttractionName = a.Name

            }).ToList();

            return touristAttractionScheduleModels;
        }


        public int TotalCity()
        {
            int i = _dbContext.DictionaryCity.Count();
            return i;
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

            if (!string.IsNullOrEmpty(searchText) )

                 return _dbContext.Schedule.Where(a => a.TouristAttraction.Name == searchText).Count();
            else
                return _dbContext.Schedule.Count();
        }

        public List<ScheduleTouristAttractionModel> GetDictionaryScheduleFiltred(int page, int pageSize, string searchString)
        {

            int pageSkip = (page - 1) * pageSize;
            List<ScheduleTouristAttractionModel> schedulefiltered = _dbContext.Schedule
                .Where(a => a.TouristAttraction.Name == searchString || a.TouristAttraction.Name.StartsWith(searchString)).
                Select(a => new ScheduleTouristAttractionModel()
                {
                    ScheduleId=a.ScheduleId,
                    Day = a.Day,
                    StartHour = a.StartHour,
                    EndHour = a.EndHour,
                    Season = a.Season.DictionarySeasonName,
                    TouristAttractionName = a.TouristAttraction.Name



                }).Skip(pageSkip).Take(pageSize).ToList();

            return schedulefiltered;
        }

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
            catch (Exception ex)
            {
                return "Ghinion frate! Exista un judet in aceasta tara.";
            }
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
            catch(Exception e)
            {
                return "Trebuie sa completati campurile";
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
                    CountryId= a.DictionaryCountryId
                }).First();

            return dictionaryCountyTypeModel;
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
    }
}
