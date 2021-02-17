using BackendTestTask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Services.SourceService
{
    public interface ISourceService
    {
        Source GetSourceByName(string name);
    }
}
