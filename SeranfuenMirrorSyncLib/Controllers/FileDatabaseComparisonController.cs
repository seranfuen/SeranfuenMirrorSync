using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class FileDatabaseComparisonController
    {
        public FileDatabaseComparisonController(string sourceRoot, string mirrorRoot)
        {
            SourceRoot = sourceRoot;
            MirrorRoot = mirrorRoot;
        }

        public string SourceRoot
        {
            get;
            private set;
        }

        public string MirrorRoot
        {
            get;
            private set;
        }

        public FileDatabase SourceFileDatabase
        {
            get;
            private set;
        }

        public FileDatabase MirrorFileDatabase
        {
            get;
            private set;
        }

        public List<IFileFilter> FileFilters
        {
            get;
            internal set;
        }

        public List<IFileFilter> DirectoryFilters
        {
            get;
            internal set;
        }

        public void LoadDatabases()
        {
            List<Task> listTasks = new List<Task>();
            listTasks.Add(Task.Factory.StartNew(() => SourceFileDatabase = LoadDatabase(SourceRoot)));
            listTasks.Add(Task.Factory.StartNew(() => MirrorFileDatabase = LoadDatabase(MirrorRoot)));
            Task.WaitAll(listTasks.ToArray());
        }

        public List<FileSyncAction> CalculateSyncActions()
        {
            if (SourceFileDatabase == null || MirrorFileDatabase == null)
            {
                throw new InvalidOperationException("The databases haven't been loaded");
            }

            List<Task> listTasks = new List<Task>();
            var listActions = new List<FileSyncAction>();

            listTasks.Add(Task.Factory.StartNew(() =>
            {
                var comparer = new FileComparisonDecider(SourceRoot, MirrorRoot);
                var localList = new List<FileSyncAction>();
                foreach (var entry in SourceFileDatabase.DatabaseEntries)
                {
                    localList.Add(comparer.DecideAction(entry, MirrorFileDatabase.GetEntryByVirtualPath(entry.VirtualPath)));
                }
                lock (this)
                {
                    listActions.AddRange(localList);
                }
            }));

            listTasks.Add(Task.Factory.StartNew(() =>
            {
                var comparer = new FileComparisonDecider(SourceRoot, MirrorRoot);
                var localList = new List<FileSyncAction>();

                var onlyMirror = MirrorFileDatabase.Minus(SourceFileDatabase);
                foreach (var entry in onlyMirror.DatabaseEntries)
                {
                    localList.Add(comparer.DecideAction(FileDatabaseEntry.NonExistentFile, entry));
                }

                lock (this)
                {
                    listActions.AddRange(localList);
                }
            }));

            Task.WaitAll(listTasks.ToArray());
            return listActions;
        }

        private FileDatabase LoadDatabase(string rootPath)
        {
            var crawler = new FileDatabaseCrawler(rootPath);
            crawler.FileFilters = FileFilters;
            crawler.DirectoryFilters = DirectoryFilters;
            crawler.LoadFileDatabase();
            return crawler.FileDatabase;
        }
    }
}