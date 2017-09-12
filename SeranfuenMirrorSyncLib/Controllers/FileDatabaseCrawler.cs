using SeranfuenMirrorSyncLib.Data;
using SeranfuenMirrorSyncLib.Utils;
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

        public List<IFileFilter> FileFilters
        {
            get;
            set;
        }

        public List<IFileFilter> DirectoryFilters
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

        public FileDatabase FileDatabase
        {
            get
            {
                return new FileDatabase(FileDatabaseEntries);
            }
        }

        public void LoadFileDatabase()
        {
            if (Directory.Exists(RootPath))
            {
                if (FileFilters == null) FileFilters = new List<IFileFilter>();
                if (DirectoryFilters == null) DirectoryFilters = new List<IFileFilter>();
                CrawlInternal(RootPath);
            }
        }

        protected void CrawlInternal(string path)
        {
            try
            {
                foreach (var file in Directory.GetFiles(path).Where(file => FileFilters.All(filter => !filter.ShouldFilter(file))))
                {
                    var entry = new FileDatabaseEntry(new FileInfo(file));
                    entry.VirtualPath = FileDatabaseEntry.GetVirtualPath(entry.LocalPath, _rootPath);

                    lock (this)
                    {
                        databaseEntries.Add(entry);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {

            }
            try
            {
                foreach (var dir in Directory.GetDirectories(path).Where(dir => DirectoryFilters.All(filter => !filter.ShouldFilter(dir))))
                {
                    CrawlInternal(dir);
                }
            }
            catch (UnauthorizedAccessException)
            {

            }
        }
    }
}
