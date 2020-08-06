using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Tool.DataBase
{
    public static class DataBaseTool
    {
        public static DataTable ToDataTable(string json)
        {
            return JsonConvert.DeserializeObject<DataTable>(json);
        }
    }
}
