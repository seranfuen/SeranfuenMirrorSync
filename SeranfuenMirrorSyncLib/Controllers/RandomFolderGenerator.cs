using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class RandomFolderGenerator
    {
        private const string ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private string _rootFolder;
        private int _maxDepth;
        private int _maxBreadth;
        private Random _random = new Random();
        private DateTime _lastStart;
        private DateTime _lastEnd;
        private List<Exception> _listExceptions;

        public RandomFolderGenerator(string rootFolder, int maxDepth = 5, int maxBreadth = 4)
        {
            _rootFolder = rootFolder;
            _maxDepth = maxDepth;
            _maxBreadth = maxBreadth;
        }

        public TimeSpan LastGenDuration
        {
            get;
            private set;
        }

        public bool LastGenFailed
        {
            get
            {
                return _listExceptions.Any();
            }
        }

        public int GeneratedFoldersCount
        {
            get;
            private set;
        }

        public void Start()
        {
            _lastStart = DateTime.Now;
            _listExceptions = new List<Exception>();
            GeneratedFoldersCount = 0;
            CreateRandomFolders(_rootFolder, 0);
            _lastEnd = DateTime.Now;
            LastGenDuration = _lastEnd - _lastStart;
            if (_listExceptions.Any())
            {
                throw new AggregateException(_listExceptions);
            }
        }

        private string RandomName()
        {
            var ofSb = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                ofSb.Append(ALPHABET[_random.Next(ALPHABET.Length)]);
            }
            return ofSb.ToString();
        }

        private void CreateRandomFolders(string path, int depth)
        {
            if (depth > _maxDepth)
            {
                return;
            }

            Parallel.For(0, _maxBreadth, (i) =>
            {
                try
                {
                    var newPath = Path.Combine(path, RandomName());
                    Directory.CreateDirectory(newPath);
                    CreateRandomFolders(newPath, depth + 1);
                    lock (this)
                    {
                        GeneratedFoldersCount++;
                    }
                }
                catch (Exception ex)
                {
                    lock(this)
                    {
                        _listExceptions.Add(ex);
                    }
                }
            });
        }
    }
}
