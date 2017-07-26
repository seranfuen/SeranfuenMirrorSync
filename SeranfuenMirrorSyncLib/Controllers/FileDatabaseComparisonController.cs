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

        public bool IsFaulted
        {
            get
            {
                return Exception != null;
            }
        }

        public AggregateException Exception
        {
            get;
            private set;
        }

        public void LoadDatabases()
        {
            try
            {
                List<Task> listTasks = new List<Task>();
                listTasks.Add(Task.Factory.StartNew(() => SourceFileDatabase = LoadDatabase(SourceRoot)));
                listTasks.Add(Task.Factory.StartNew(() => MirrorFileDatabase = LoadDatabase(MirrorRoot)));
                Task.WaitAll(listTasks.ToArray());
            }
            catch (AggregateException ex)
            {
                Exception = ex;
            }

        }

        private FileDatabase LoadDatabase(string rootPath)
        {
            var crawler = new FileDatabaseCrawler(rootPath);
            crawler.LoadFileDatabase();
            return crawler.FileDatabase;
        }
    }
}