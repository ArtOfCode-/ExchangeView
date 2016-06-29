using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace ExchangeStats
{
    class ApiCache
    {
        public static string BaseCachePath { get; set; }

        public static bool HasValidItem(string requestUri)
        {
            string[] cacheInfo = File.ReadAllLines(Path.Combine(BaseCachePath, "CacheInfo.dat"));
            foreach (string line in cacheInfo)
            {
                string[] splat = line.Split(' ');
                if (splat.Length == 3 && splat[0] == requestUri)
                {
                    DateTime expiration;
                    if (DateTime.TryParse(splat[1].Replace('-', ' '), out expiration) && expiration > DateTime.Now)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static Stream GetDataStream(string requestUri)
        {
            string[] cacheInfo = File.ReadAllLines(Path.Combine(BaseCachePath, "CacheInfo.dat"));
            foreach (string line in cacheInfo)
            {
                string[] splat = line.Split(' ');
                if (splat.Length == 3 && splat[0] == requestUri)
                {
                    string cacheFile = Path.Combine(BaseCachePath, splat[2]);
                    if (File.Exists(cacheFile))
                    {
                        return new FileStream(cacheFile, FileMode.Open);
                    }
                }
            }

            return null;
        }

        public static void AddCacheItem(string requestUri, int expiration, string contents)
        {
            string fileName = GenerateFileName(7);
            DateTime expiresAt = DateTime.Now.AddSeconds((double)expiration);
            File.AppendAllText(Path.Combine(BaseCachePath, "CacheInfo.dat"),
                string.Format("{0} {1} {2}\n", requestUri, expiresAt.ToString().Replace(' ', '-'), fileName));

            using (FileStream stream = File.Create(Path.Combine(BaseCachePath, fileName)))
            {
                byte[] data = Encoding.Unicode.GetBytes(contents);
                stream.Write(data, 0, data.Length);
            }
        }

        private static string GenerateFileName(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
