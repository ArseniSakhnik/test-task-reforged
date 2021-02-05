using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Helpers
{
    /// <summary>
    /// Класс для получения настроек
    /// </summary>
    public class AppSettings
    {
        public string FinnhubKey { get; set; }
        public string Secret { get; set; }
        public int UpdateMinutes { get; set; }
    }
}
