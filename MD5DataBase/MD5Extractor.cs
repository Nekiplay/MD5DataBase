using System.IO;
using System.Net;

namespace MD5DataBase
{
    public class MD5Extractor
    {
        public MD5Info GetInfoFromCloud(FileInfo file)
        {
            string hash = HashUtils.GetMD5Hash(file);
            return GetInfoFromCloud(hash);
        }
        public MD5Info GetInfoFromCloud(string md5)
        {
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString("https://github.com/Nekiplay/MD5List/raw/main/files/" + md5 + ".json");
                return MD5Info.FromJson(json);
            }
        }
    }
}
