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
        public SHA3TextInfo GetTextInfoFromCloud(string SHA3256, Encoding encoding)
        {
            string link = "https://github.com/Nekiplay/SHA3List/raw/main/text/";
            if (encoding == Encoding.Unicode)
                link += "unicode/";
            else if (encoding == Encoding.BigEndianUnicode)
                link += "bigendianunicode/";
            else if (encoding == Encoding.ASCII)
                link += "ascii/";
            else if (encoding == Encoding.UTF7)
                link += "utf7/";
            else if (encoding == Encoding.UTF8)
                link += "utf8/";
            else if (encoding == Encoding.UTF32)
                link += "utf32/";
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString(link + SHA3256 + ".json");
                return SHA3TextInfo.FromJson(json);
            }
        }
    }
}
