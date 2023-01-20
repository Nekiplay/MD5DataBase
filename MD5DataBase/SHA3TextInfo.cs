using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD5DataBase
{
    public class SHA3TextInfo
    {
        public string text { get; set; }
        public HashInfo hash = new HashInfo();

        public string ToJson()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new JavaScriptDateTimeConverter());
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            settings.Formatting = Formatting.Indented;
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, settings);
        }

        public static SHA3TextInfo FromJson(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new JavaScriptDateTimeConverter());
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            settings.Formatting = Formatting.Indented;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<SHA3TextInfo>(json, settings);
        }
    }
}
