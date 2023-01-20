using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace MD5DataBase
{
    public class MD5Info 
    {
        public Hash hash = new Hash();
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Game { get; set; }
        public string Site { get; set; }
        public List<Type> Types = new List<Type>();

        public List<Hash> Libs = new List<Hash>();
        public List<Hash> Used = new List<Hash>();

        public enum Type
        {
            Cheat,
            Virus,
            Tool,
            Program,
            Crack,
            Loader,
            Mod,
            Game,
            ResourcePack,
            CP,
            Porno,
            Hentai,
            ScreenShare,
        }

        public class Hash
        {
            public string MD5;
            public string SHA256;
        }

        public string ToJson()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new JavaScriptDateTimeConverter());
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            settings.Formatting = Formatting.Indented;
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, settings);
        }

        public static MD5Info FromJson(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new JavaScriptDateTimeConverter());
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            settings.Formatting = Formatting.Indented;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<MD5Info>(json, settings);
        }
    }
}
