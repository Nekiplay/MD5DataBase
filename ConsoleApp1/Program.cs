using MD5DataBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "FileInfo by MD5";
            SHA3Extractor extractor = new SHA3Extractor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("File path: ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string filePath = Console.ReadLine().Replace("\"", "");
            FileInfo fileInfo = new FileInfo(filePath);
            try
            {
                if (fileInfo.Exists)
                {
                    SHA3FileInfo info = extractor.GetFileInfoFromCloud(fileInfo);
                    Console.Clear();
                    Console.Title = "FileInfo by MD5 | " + info.hash.SHA256;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(info.ToJson());
                }
                else
                {
                    SHA3FileInfo info = extractor.GetFileInfoFromCloud(filePath);
                    Console.Clear();
                    Console.Title = "FileInfo by MD5 | " + info.hash.SHA256;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(info.ToJson());
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not found in cloud, creating new file info");
                Console.WriteLine();
                SHA3FileInfo info = new SHA3FileInfo();
                info.hash.MD5 = HashUtils.GetMD5Hash(fileInfo);
                info.hash.SHA256 = HashUtils.GetSHA2565Hash(fileInfo);
                info.Extension = fileInfo.Extension;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("File name: ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                info.Name = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Version: ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                info.Version = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Description: ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                info.Description = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Game: ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                info.Game = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Site: ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                info.Site = Console.ReadLine();

                string[] names = Enum.GetNames(typeof(SHA3FileInfo.Type));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Available types: ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(String.Join(",", names) + "\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Types: ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                string typestring = Console.ReadLine();
                string[] splited = typestring.Split(',');
                info.Types = new List<SHA3FileInfo.Type>();
                foreach (string s in splited)
                {
                    SHA3FileInfo.Type type = SHA3FileInfo.Type.Cheat;
                    if (Enum.TryParse(s, out type))
                    {
                        info.Types.Add(type);
                    }
                }
                string last_lib = "";
                string last_used = "";
                info.Used = new List<HashInfo>();
                info.Libs = new List<HashInfo>();
                newlib:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Current libs: ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(info.Libs.Count + "\n");
                if (last_lib != "")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Last library: ");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(last_lib + "\n");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Add new lib? yes or no: ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                string add = Console.ReadLine();
                if (add == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Library path: ");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    string path = Console.ReadLine();
                    FileInfo info1 = new FileInfo(path.Replace("\"", ""));
                    HashInfo hash = new HashInfo();
                    hash.SHA256 = HashUtils.GetSHA2565Hash(info1);
                    hash.MD5 = HashUtils.GetMD5Hash(info1);
                    info.Libs.Add(hash);
                    last_lib = path;
                    Console.Clear();
                    goto newlib;
                }
                else
                {
                    newused:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Current used programs: ");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(info.Libs.Count + "\n");
                    if (last_used != "")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Last used program: ");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(last_used + "\n");
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Add new programs that use this file? yes or no: ");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    string add2 = Console.ReadLine();
                    if (add2 == "yes")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Program path: ");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        string path = Console.ReadLine();
                        FileInfo info1 = new FileInfo(path.Replace("\"", ""));
                        HashInfo hash = new HashInfo();
                        hash.SHA256 = HashUtils.GetSHA2565Hash(info1);
                        hash.MD5 = HashUtils.GetMD5Hash(info1);
                        info.Used.Add(hash);
                        last_used = path;
                        Console.Clear();
                        goto newused;
                    }
                    else
                    {


                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(info.ToJson());
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
