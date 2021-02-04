﻿using BackendTestTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.APIFetchersServices.FinnhubAPIService
{
    public interface IFinnhubAPIService
    {
        ValueTask<FinnhubApiResponse> GetCompanyProfileByTicker(string ticker);
    }
}
