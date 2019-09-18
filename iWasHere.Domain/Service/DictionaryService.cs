using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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





    }
}
