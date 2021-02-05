using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Models.Responses
{
    /// <summary>
    /// Модель ответа с сервера Moex
    /// </summary>
    public class MoexApiResponse
    {
        public Securities securities { get; set; }
    }
}
