using BackendTestTask.Entities;
using BackendTestTask.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.APIFetchersServices.MoexAPIService
{
    /// <summary>
    /// Интерфейс для добавления сервиса
    /// </summary>
    public interface IMoexAPIService
    {
        //ValueTask<MoexApiResponse> GetMoexCompanies();
        ValueTask<MoexApiResponse> GetCompanyProfileByTicker(string ticker);
    }
}
