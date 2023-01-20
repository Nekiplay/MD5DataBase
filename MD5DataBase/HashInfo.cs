using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD5DataBase
{
    public class HashInfo
    {
        public string MD5;
        public string SHA256;
        public string SHA3256;

        public HashInfo(string md5, string sha256, string sha3256)
        {
            this.MD5 = md5;
            this.SHA256 = sha256;
            this.SHA3256 = sha3256;
        }

        public HashInfo()
        {

        }
    }
}
