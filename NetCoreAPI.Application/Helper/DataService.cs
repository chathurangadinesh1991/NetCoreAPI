using Microsoft.Extensions.Options;
using NetCoreAPI.Domain.Helper;
using NetCoreAPI.Domain.Models;
using NetCoreAPI.Domain.Services;
using NetCoreAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreAPI.Application.Helper
{
    public class DataService : IDataService
    {
        private SSNDBContext _context;
        private readonly AppSettings _appSettings;

        public DataService(SSNDBContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public List<Tuple<string, string>> GetAllData()
        {
            try
            {
               return _context.UserInfos
                    .Select(o => new Tuple<string,string>(o.FirstName, o.LastName))
                    .ToList();
            }
            catch (Exception)
            {                
                return null;
            }            
        }

        public bool SaveData(SaveDataRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
