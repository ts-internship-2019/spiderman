using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class DictionaryCityModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public string CountryName { get; set; }
        public DictionaryCityModel() { }
        public DictionaryCityModel(string CityName, int CountyId) {

            this.CityName=CityName;
            this.CountyId = CountyId;
        }

    }
}
