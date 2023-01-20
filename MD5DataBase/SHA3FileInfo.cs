using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace MD5DataBase
{
    public class SHA3FileInfo 
    {
        [JsonProperty("Hash")]
        public HashInfo hash = new HashInfo();
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Game { get; set; }
        public string Site { get; set; }
        public List<Type> Types = new List<Type>();
        public List<PornographyType> PornographyTypes = new List<PornographyType>();

        public List<HashInfo> Libs = new List<HashInfo>();
        public List<HashInfo> Used = new List<HashInfo>();
        public enum PornographyType
        {
            Default,
            CP,
            Hentai,
            Loli,
            Elf,
            Furry,
            Other,
        }
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
            ScreenShare,
        }

        public string ToJson()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new JavaScriptDateTimeConverter());
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            settings.Formatting = Formatting.Indented;
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, settings);
        }

        public static SHA3FileInfo FromJson(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new JavaScriptDateTimeConverter());
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            settings.Formatting = Formatting.Indented;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<SHA3FileInfo>(json, settings);
        }
    }
}
