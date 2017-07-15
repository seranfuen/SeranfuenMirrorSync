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

        public RandomFolderGenerator(string rootFolder, int maxDepth = 5, int maxBreadth = 20)
        {
            _rootFolder = rootFolder;
            _maxDepth = maxDepth;
            _maxBreadth = maxBreadth;
        }

        public void Start()
        {
            CreateRandomFolders(_rootFolder, 0);
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

            Parallel.For(0, _random.Next(1, _maxBreadth), (i) =>
            {
                var newPath = Path.Combine(path, RandomName());
                Directory.CreateDirectory(newPath);
                CreateRandomFolders(newPath, depth+1);
            });
        }
    }
}
