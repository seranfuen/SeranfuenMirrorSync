using SeranfuenMirrorSyncLib.Data;
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
                Parallel.ForEach(Directory.GetFiles(path).Where(file => FileFilters.All(filter => !filter.ShouldFilter(file))), (file) =>
                {
                    var entry = new FileDatabaseEntry(new FileInfo(file));
                    entry.VirtualPath = FileDatabaseEntry.GetVirtualPath(entry.LocalPath, _rootPath);

                    lock (this)
                    {
                        databaseEntries.Add(entry);
                    }
                });
            }
            catch (UnauthorizedAccessException)
            {

            }
            try
            {
                Parallel.ForEach(Directory.GetDirectories(path).Where(dir => DirectoryFilters.All(filter => !filter.ShouldFilter(dir))), (dir) =>
                {
                    CrawlInternal(dir);
                });
            }
            catch (UnauthorizedAccessException)
            {

            }
        }
    }
}
