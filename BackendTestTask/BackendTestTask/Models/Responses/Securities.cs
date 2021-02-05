using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Models.Responses
{
    /// <summary>
    /// Вспомогательный класс для модели ответа с сервера moex
    /// </summary>
    public class Securities
    {
        public List<string> columns { get; set; }
        public List<List<object>> data { get; set; }
    }
}
