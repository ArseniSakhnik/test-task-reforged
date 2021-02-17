using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Services.SourceService
{
    public class SourceService : ISourceService
    {
        private DataContext DataContext { get; set; }

        public SourceService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public Source GetSourceByName(string name)
        {
            try
            {
                var source = DataContext.Sources.Where(s => s.Name == name).SingleOrDefault();

                return source;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
