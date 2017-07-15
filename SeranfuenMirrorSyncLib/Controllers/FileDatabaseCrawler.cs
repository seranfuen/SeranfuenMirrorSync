using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class FileDatabaseCrawler
    {
        private string _rootPath;
        private List<FileDatabaseEntry> databaseEntries;

        public FileDatabaseCrawler(string rootPath)
        {
            _rootPath = rootPath;
            databaseEntries = new List<FileDatabaseEntry>();
        }

        public string RootPath
        {
            get { return _rootPath; }
        }

        public List<Regex> FileFilters
        {
            get;
            set;
        }

        public List<Regex> DirectoryFilters
        {
            get;
            set;
        }

        public bool IncludeHash
        {
            get;
            set;
        }

        public List<FileDatabaseEntry> FileDatabaseEntries
        {
            get
            {
                return databaseEntries.ToList();
            }
        }

        public void LoadFileDatabase()
        {
            CrawlInternal(RootPath);
        }

        protected string CalculateMD5Hash(string file)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(file);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        protected void CrawlInternal(string path)
        {
            try
            {
                Parallel.ForEach(Directory.GetFiles(path), (file) =>
                {
                    var entry = new FileDatabaseEntry(new FileInfo(file));
                    entry.VirtualPath = FileDatabaseEntry.GetVirtualPath(entry.LocalPath, _rootPath);
                    if (IncludeHash)
                    {
                        entry.Hash = CalculateMD5Hash(file);
                    }

                    lock (this)
                    {
                        databaseEntries.Add(entry);
                    }
                });

                //foreach (var file in Directory.GetFiles(path))
                //{
                //    lock (this)
                //    {
                //        databaseEntries.Add(new FileDatabaseEntry(new FileInfo(file)));
                //    }
                //}
            }
            catch (UnauthorizedAccessException e)
            {

            }
            try
            {
                Parallel.ForEach(Directory.GetDirectories(path), (dir) =>
                {
                    CrawlInternal(dir);
                });

                //foreach (var dir in Directory.GetDirectories(path))
                //{
                //    CrawlInternal(dir);
                //}
            }
            catch (UnauthorizedAccessException e)
            {

            }
        }
    }
}
