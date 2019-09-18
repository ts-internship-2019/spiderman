using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class DictionaryCityModel
    {
        public int Id { get; set; }
        public string CityName { get; set; }
  
        public string CountyName { get; set; }
        public string CountryName { get; set; }
    }
}
