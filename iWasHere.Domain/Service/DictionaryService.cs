using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
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
        public int GetCountDictionaryCurrency()
        {
            return _dbContext.DictionaryCurrency.Count();
        }
    }
}
