using NetCoreAPI.Domain.Models;
using System;
using System.Collections.Generic;

namespace NetCoreAPI.Domain.Services
{
    public interface IDataService
    {
        public List<Tuple<string, string>> GetAllData();
        public bool SaveData(SaveDataRequest model);      
    }
}