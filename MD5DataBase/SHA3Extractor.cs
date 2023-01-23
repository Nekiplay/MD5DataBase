using System.IO;
using System.Net;
using System.Text;
using static System.Net.WebRequestMethods;

namespace MD5DataBase
{
    public class SHA3Extractor
    {
        public SHA3FileInfo GetFileInfoFromCloud(FileInfo file)
        {
            string hash = HashUtils.GetMD5Hash(file);
            return GetFileInfoFromCloud(hash);
        }
        public SHA3FileInfo GetFileInfoFromCloud(string SHA3256)
        {
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString("https://github.com/Nekiplay/SHA3List/raw/main/files/" + SHA3256 + ".json");
                return SHA3FileInfo.FromJson(json);
            }
        }
        public SHA3byFileName GetFileHashByNameFromCloud(string filename)
        {
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString("https://github.com/Nekiplay/SHA3List/raw/main/file_name_to_sha3/" + filename + ".json");
                return SHA3byFileName.FromJson(json);
            }
        }
        public SHA3TextInfo GetTextInfoFromCloud(string SHA3256)
        {
            string link = "https://github.com/Nekiplay/SHA3List/raw/main/text/";
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString(link + SHA3256 + ".json");
                return SHA3TextInfo.FromJson(json);
            }
        }
    }
}
