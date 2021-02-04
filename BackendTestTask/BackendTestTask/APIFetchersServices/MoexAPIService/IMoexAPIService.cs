﻿using BackendTestTask.Entities;
using BackendTestTask.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.APIFetchersServices.MoexAPIService
{
    public interface IMoexAPIService
    {
        ValueTask<MoexApiResponse> GetMoexCompanies();
    }
}
