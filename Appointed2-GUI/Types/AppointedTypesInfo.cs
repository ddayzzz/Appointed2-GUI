using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Appointed2_GUI.Types
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AppointedTypesInfo
    {
        INT,
        FLOAT,
        STRING,
        LIST,
        NONE
    }
}
